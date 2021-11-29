using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterCubeListener : MonoBehaviour
{
    public int masterCubeDestroyed = 0;

    [SerializeField]
    private int masterCubeRequirement;

    [SerializeField]
    GameObject masterForceField;

    private Vector3
        targetPosition = new Vector3(116.5378f, 61.54643f, 112.2526f);

    GameObject player;

    [SerializeField]
    private string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (masterCubeDestroyed >= masterCubeRequirement)
        {
            masterCubeDestroyed = 0;
            StartCoroutine(ExampleCoroutine());

        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
                    GameObject player = GameObject.FindGameObjectWithTag("Player");

            SceneManager.LoadScene (sceneToLoad);
            Physics.gravity = new Vector3(0, -9.81f, 0);

            player.SetActive(false);
            player.transform.position = targetPosition;

            //player.rotation=Quaternion.identity;
            player.transform.rotation = Quaternion.Euler(0, -120, 0);
            player.SetActive(true);

            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void MasterCubeIncrementNumber()
    {
        masterCubeDestroyed++;
    }
}
