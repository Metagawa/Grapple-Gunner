using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private InputActionReference menuButtonReference;

    [SerializeField]
    private string sceneToLoad;

    private Vector3
        targetPosition = new Vector3(116.5378f, 61.54643f, 112.2526f);

    private string currentScene;

    private SpringJoint joint;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        menuButtonReference.action.started += MenuOpen;
        menuButtonReference.action.canceled -= MenuOpen;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void MenuOpen(InputAction.CallbackContext obj)
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (
            sceneToLoad != "None" &&
            currentScene != sceneToLoad &&
            currentScene != "Main"
        )
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            joint = player.gameObject.GetComponent<SpringJoint>();
            if (!joint)
            {
                SceneManager.LoadScene (sceneToLoad);
                Physics.gravity = new Vector3(0, -9.81f, 0);

                player.SetActive(false);
                player.transform.position = targetPosition;

                //player.rotation=Quaternion.identity;
                player.transform.rotation = Quaternion.Euler(0, -120, 0);
                player.SetActive(true);

                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
