using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDecrementerOnDestroy : MonoBehaviour
{

    LevelEndListener levelEndListener;

    // Start is called before the first frame update
    void Start()
    {
    levelEndListener=GameObject.FindGameObjectWithTag("Handler").GetComponent<LevelEndListener>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy() {
    levelEndListener.IncrementNumber(); 

    }
}
