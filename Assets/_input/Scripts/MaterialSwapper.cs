using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class MaterialSwapper : MonoBehaviour
{
    public Material CorrectMat;
    private MeshRenderer m_meshRenderer;
    public string TargetGameObjectTag = "Wall";
    private void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
 
            m_meshRenderer.material = CorrectMat;
        
    }
}