using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrusterModule : MonoBehaviour
{

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private InputActionReference thrustActionReference;
    public bool thrust = false;
    private Vector3 speed;
    [SerializeField] private float thrustForce = 500.0f;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        thrustActionReference.action.started += ThrustBoost;
        thrustActionReference.action.canceled += ThrustCancel;
    }
    void ThrustBoost(InputAction.CallbackContext obj)
    {

        ///playerBody.AddForce(Camera.main.transform.forward * thrustForce);
        Vector3 dir = Camera.main.transform.forward;
        float speed =thrustForce;
        playerBody.velocity = speed * dir;
        Vector3 secondaryTemp=playerBody.velocity;
    }
    void ThrustCancel(InputAction.CallbackContext obj)
    {
        i=0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
