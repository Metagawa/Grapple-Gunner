using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesListener : MonoBehaviour
{
    public int cubesDestroyed = 0;

    [SerializeField]
    private int cubesRequirement;

    [SerializeField]
    GameObject ActiveSpheres;

    [SerializeField]
    GameObject InactiveSphere;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cubesDestroyed >= cubesRequirement)
        {
            cubesDestroyed = 0;
            ActiveSpheres.SetActive(true);
            InactiveSphere.SetActive(false);
            /// More Code needed here to do something
        }
    }

    public void CubesIncrementNumber()
    {
        cubesDestroyed++;
    }
}
