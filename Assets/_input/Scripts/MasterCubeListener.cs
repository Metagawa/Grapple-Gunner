using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCubeListener : MonoBehaviour
{
    public int masterCubeDestroyed = 0;

    [SerializeField]
    private int masterCubeRequirement;

    [SerializeField]
    GameObject masterForceField;

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
            masterForceField.SetActive(false);
        }
    }

    public void MasterCubeIncrementNumber()
    {
        masterCubeDestroyed++;
    }
}
