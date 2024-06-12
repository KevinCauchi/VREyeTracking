using UnityEngine;
using UnityEngine.Events; // Added namespace for UnityEvent

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EyeInteractable : MonoBehaviour
{
    public bool IsHovered { get; set; }

    [SerializeField]
    private UnityEvent<GameObject> onObjectHover;

    [SerializeField]
    private Material OnHoverActiveMaterial;

    [SerializeField]
    private Material OnHoverInactiveMaterial;

    private MeshRenderer meshRenderer; // Added MeshRenderer

    void Start() => meshRenderer = GetComponent<MeshRenderer>();

    // Update is called once per frame
    void Update()
    {
        if (IsHovered)
        {
            meshRenderer.material = OnHoverActiveMaterial;
            onObjectHover?.Invoke(gameObject);
        }
        else
        {
            meshRenderer.material = OnHoverInactiveMaterial;
        }
    }
}
