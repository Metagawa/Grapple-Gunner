using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponListener : MonoBehaviour
{
    [SerializeField]
    GameObject LeftSword;

    [SerializeField]
    GameObject LeftGun;

    [SerializeField]
    GameObject RightSword;

    [SerializeField]
    GameObject RightGun;

    private string currentScene;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Sword Mode")
        {
            LeftGun.SetActive(false);
            LeftSword.SetActive(true);
            RightGun.SetActive(false);
            RightSword.SetActive(true);
        }
        else
        {
            LeftGun.SetActive(true);
            LeftSword.SetActive(false);
            RightGun.SetActive(true);
            RightSword.SetActive(false);
        }
    }
}
