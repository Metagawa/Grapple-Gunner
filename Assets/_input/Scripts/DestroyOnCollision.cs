using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (
            other.tag != "Sliceable" &&
            other.tag != "Player" &&
            other.tag != "PlayerTag"
        )
        {
            Destroy (gameObject);
        }
    }
}
