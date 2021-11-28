using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    private InputActionReference blasterActionReference;

    public GameObject projectile;

    public float launchVelocity = 700f;

    private string currentScene;

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
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "Sword Mode")
        {
            GameObject sword =
                Instantiate(projectile, transform.position, transform.rotation);
            sword
                .GetComponent<Rigidbody>()
                .AddRelativeForce(new Vector3(0, 0, launchVelocity));
            Destroy(sword, 20f);
        }
    }
}
