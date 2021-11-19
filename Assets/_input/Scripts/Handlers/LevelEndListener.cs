using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndListener : MonoBehaviour
{

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
            SceneManager.LoadScene("TheEnd");
        }   
    }

    public void IncrementNumber()
{
    slicedCount++;
}

}
