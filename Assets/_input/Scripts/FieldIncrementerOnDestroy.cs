using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldIncrementerOnDestroy : MonoBehaviour
{
    [SerializeField]
    ForcefieldListener forcefieldListener;

    // Start is called before the first frame update
    void Start()
    {
        ///forcefieldListener=GameObject.FindGameObjectWithTag("Handler").GetComponent<ForcefieldListener>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {
        forcefieldListener.FieldsIncrementNumber();
    }
}
