using UnityEngine;
using UnityEngine.InputSystem;

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

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        ///grappleActionReference.action.performed += StopGrapple;
        grappleActionReference.action.started += StartGrapple;
        grappleReelReference.action.started += ReelGrapple;
        grappleReelReference.action.canceled += StopReelGrapple;
        grappleActionReference.action.canceled += StopGrapple;
    }
    void Update()
    {

        /// if (Input.GetMouseButtonDown(0)) {
        ///     StartGrapple();
        /// }
        /// else if (Input.GetMouseButtonUp(0)) {
        ///     StopGrapple();
        /// }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();

    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
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

            //The distance grapple will try to keep from grapple point. 
            ///joint.maxDistance = distanceFromPoint * 0.2f;
            /// joint.minDistance = distanceFromPoint * 0.25f;
            joint.maxDistance = distanceFromPoint;
            joint.minDistance = 1f;

            //Adjust these values to fit your game.
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
        while (reeling && joint.maxDistance > 0)
        {
            joint.maxDistance--;
        }
    }

    void StopReelGrapple(InputAction.CallbackContext obj)
    {
        reeling = false;
    }
    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple(InputAction.CallbackContext obj)
    {
        lr.positionCount = 0;
        Destroy(joint);
    }


    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
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