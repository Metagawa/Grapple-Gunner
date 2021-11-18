using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class SlowGrapple : MonoBehaviour
{
    [SerializeField] private InputActionReference grappleActionReference;
    [SerializeField] private InputActionReference grappleReelReference;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform origin, target, player;
    private float maxDistance = 250f;
    private SpringJoint joint;
    private bool reeling = false;
    private float distanceFromPoint = 0f;
    public float reelingSpeed = 1f;
    public static bool gripBool;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Start()
    {
        grappleActionReference.action.started += StartGrapple;
        grappleReelReference.action.started += ReelGrapple;
        grappleActionReference.action.canceled += StopGrapple;
    }
    void Update()
    {

    }

    void LateUpdate()
    {
        DrawRope();

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
            joint.minDistance = 1f;

            joint.spring = 5.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = origin.position;
        }
    }

    void ReelGrapple(InputAction.CallbackContext obj)
    {
        reeling=true;
        if (!joint) return;
            joint.maxDistance = distanceFromPoint;
            joint.minDistance = 1f;
        while (joint.maxDistance>joint.minDistance)
        {
            if (reeling==false){
                break;}
            joint.maxDistance=joint.maxDistance-0.01f;
        }
    }

    void StopGrapple(InputAction.CallbackContext obj)
    {
        lr.positionCount = 0;
        Destroy(joint);
    }


    private Vector3 currentGrapplePosition;

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