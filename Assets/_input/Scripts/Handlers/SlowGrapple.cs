using System.Threading;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class SlowGrapple : MonoBehaviour
{
    [SerializeField] private InputActionReference grappleActionReference;
    [SerializeField] private InputActionReference grappleReelReference;
    [SerializeField] private float thrustForce = 500.0f;
    [SerializeField] private InputActionReference thrustActionReference;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform origin, target, player;
    private float maxDistance = 250f;
    private SpringJoint joint;
    private bool reeling = false;
    private float distanceFromPoint = 0f;
    public bool thrust = false;
    public int i = 0;
    [SerializeField] private Rigidbody playerBody;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Start()
    {
        thrustActionReference.action.started += ThrustBoost;
        thrustActionReference.action.canceled += ThrustCancel;
        grappleActionReference.action.started += StartGrapple;
        grappleReelReference.action.started += ReelGrapple;
        grappleActionReference.action.canceled += StopGrapple;
        grappleReelReference.action.canceled += StopReelGrapple;
    }


    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    void Update()
    {

    }

    void LateUpdate()
    {
        DrawRope();
        if (joint)
        {
        distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

        }
    }
    void ThrustBoost(InputAction.CallbackContext obj)
    {

        ///playerBody.AddForce(Camera.main.transform.forward * thrustForce);
        Vector3 dir = Camera.main.transform.forward;
        dir.y=0.5f;
        Vector3 temp=playerBody.velocity;
        temp.y=10f;
        playerBody.velocity=temp;
        float speed =playerBody.velocity.magnitude;
        playerBody.velocity = speed * dir;
        Vector3 secondaryTemp=playerBody.velocity;
        if (secondaryTemp.x<0)
        {
        secondaryTemp.x=secondaryTemp.x-thrustForce;
        } else
        {
        secondaryTemp.x=secondaryTemp.x+thrustForce;
        }

        if (secondaryTemp.z<0)
        {
        secondaryTemp.z=secondaryTemp.z-thrustForce;
        } else
        {
        secondaryTemp.z=secondaryTemp.z+thrustForce;
        }        playerBody.velocity=secondaryTemp;
        playerBody.velocity=playerBody.velocity.magnitude* dir;
    }
    void ThrustCancel(InputAction.CallbackContext obj)
    {
        i=0;
    }

    void StartGrapple(InputAction.CallbackContext obj)
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, target.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint;
            joint.minDistance = 0.0f;

            joint.spring = 5.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = origin.position;
        }
    }

    void ReelGrapple(InputAction.CallbackContext obj)
    {
        reeling = true;
        if (!joint) return;
        joint.minDistance = 0.0f;
        while (joint.maxDistance > joint.minDistance)
        {
            if (reeling == false)
            {
                break;
            }
            joint.maxDistance = joint.maxDistance - 0.01f;
        }
    }

    void StopReelGrapple(InputAction.CallbackContext obj)
    {
        reeling = false;
        joint.maxDistance = Vector3.Distance(player.position, grapplePoint);
    }
    void StopGrapple(InputAction.CallbackContext obj)
    {
        lr.positionCount = 0;
        if (joint)
        {
        Destroy(joint);
        }
    }


    private Vector3 currentGrapplePosition;
    private Vector3 speed;

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, origin.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
