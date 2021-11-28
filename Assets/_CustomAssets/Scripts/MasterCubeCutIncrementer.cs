using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCubeCutIncrementer : MonoBehaviour
{
    [SerializeField]
    MasterCubeListener masterCubeListener;

    // Start is called before the first frame update
    void Start()
    {
        ///cubesListener=GameObject.FindGameObjectWithTag("Handler").GetComponent<CubesListener>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {
        masterCubeListener.MasterCubeIncrementNumber();
    }
}
