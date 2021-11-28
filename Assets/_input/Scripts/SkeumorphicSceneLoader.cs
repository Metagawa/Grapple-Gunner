using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeumorphicSceneLoader : MonoBehaviour
{
    GameObject player;

    private string currentScene;

    [SerializeField]
    private string sceneToLoad;

    private Vector3
        targetPosition = new Vector3(116.5378f, 61.54643f, 112.2526f);

    // Start is called before the first frame update
    private SpringJoint joint;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        SceneLoader();
    }

    void SceneLoader()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (sceneToLoad != "None" && currentScene != sceneToLoad)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            joint = player.gameObject.GetComponent<SpringJoint>();
            if (!joint)
            {
                SceneManager.LoadScene (sceneToLoad);
                if (sceneToLoad == "Zero Gravity Mode")
                {
                    Physics.gravity = new Vector3(0, 0, 0);
                }
                else
                {
                    Physics.gravity = new Vector3(0, -9.81f, 0);
                }

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
