using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldListener : MonoBehaviour
{
    public int fieldsDestroyed = 0;

    [SerializeField]
    private int fieldsRequirement;

    [SerializeField]
    GameObject forcefield;

    [SerializeField]
    GameObject vulnerableMasterCube;

    [SerializeField]
    GameObject invulnerableMasterCube;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldsDestroyed >= fieldsRequirement)
        {
            Destroy(forcefield, 2f);

            fieldsDestroyed = 0;
            vulnerableMasterCube.SetActive(true);
            invulnerableMasterCube.SetActive(false);
            /// More Code needed here to do something
        }
    }

    public void FieldsIncrementNumber()
    {
        fieldsDestroyed++;
    }
}
