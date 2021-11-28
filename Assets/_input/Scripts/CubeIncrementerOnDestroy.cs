using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIncrementerOnDestroy : MonoBehaviour
{
    [SerializeField]
    CubesListener cubesListener;

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
        cubesListener.CubesIncrementNumber();
    }
}
