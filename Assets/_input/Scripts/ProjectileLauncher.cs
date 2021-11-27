using UnityEngine;
using UnityEngine.InputSystem;
public class ProjectileLauncher : MonoBehaviour
{
        [SerializeField] private InputActionReference blasterActionReference; 
   public GameObject projectile;
   public float launchVelocity = 700f;
    // Start is called before the first frame update
    void Start()
    {
    blasterActionReference.action.started += Blast;
    ///blasterActionReference.action.canceled += StopBlast;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void Blast(InputAction.CallbackContext obj) 
        {
        GameObject sword = Instantiate(projectile, transform.position, transform.rotation);    
        sword.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocity,0));
        Destroy(sword, 2f);

        }
}
