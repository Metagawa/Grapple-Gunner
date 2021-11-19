using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndListener : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 targetPosition = new Vector3(0,0,0); 
    public int slicedCount;

    // Start is called before the first frame update
    void Start()
    {

    }


    
    // Update is called once per frame
    void Update()
    {
     if (slicedCount>5)
        {
            slicedCount=0;

            SceneManager.LoadScene("TheEnd");
            player.position = targetPosition;
            player.rotation=Quaternion.identity;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }   
    }

    public void IncrementNumber()
{
    slicedCount++;
}

}
