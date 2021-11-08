using UnityEngine;
using UnityEngine.InputSystem;


public class SlowGrapple : MonoBehaviour
{
    [SerializeField] private InputActionReference grappleActionReference;

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform origin, target, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        ///grappleActionReference.action.performed += StopGrapple;

        grappleActionReference.action.started += StartGrapple;
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

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            ///joint.maxDistance = distanceFromPoint * 0.2f;
            /// joint.minDistance = distanceFromPoint * 0.25f;
            joint.maxDistance = distanceFromPoint * 0.6f;
            joint.minDistance = distanceFromPoint * 0.5f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;



            lr.positionCount = 2;
            currentGrapplePosition = origin.position;
        }
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