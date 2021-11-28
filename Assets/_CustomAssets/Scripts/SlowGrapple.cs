using UnityEngine;
using UnityEngine.InputSystem;

public class SlowGrapple : MonoBehaviour
{
    [SerializeField]
    private InputActionReference grappleActionReference;

    [SerializeField]
    private InputActionReference grappleReelReference;

    private LineRenderer lr;

    private Vector3 grapplePoint;

    public LayerMask whatIsGrappleable;

    public Transform

            origin,
            target,
            player;

    private float maxDistance = 250f;

    private SpringJoint joint;

    private bool reeling = false;

    private float distanceFromPoint = 0f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        grappleActionReference.action.started += StartGrapple;
        grappleActionReference.action.canceled += StopGrapple;

        grappleReelReference.action.started += ReelGrapple;
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

    void StartGrapple(InputAction.CallbackContext obj)
    {
        RaycastHit hit;
        if (
            Physics
                .Raycast(target.position,
                target.forward,
                out hit,
                maxDistance,
                whatIsGrappleable)
        )
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint;
            joint.minDistance = 0.0f;

            joint.spring = 40f;
            joint.damper = 50f;
            joint.massScale = 15f;

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
        if (joint)
        {
            joint.maxDistance = Vector3.Distance(player.position, grapplePoint);
        }
    }

    void StopGrapple(InputAction.CallbackContext obj)
    {
        lr.positionCount = 0;
        if (joint)
        {
            Destroy (joint);
        }
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition =
            Vector3
                .Lerp(currentGrapplePosition,
                grapplePoint,
                Time.deltaTime * 8f);

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
