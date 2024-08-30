using UnityEngine;
using UnityEngine.InputSystem;

public class CheckInteract : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_interactArea;
    [SerializeField] private InputActionAsset m_inputs;
    [SerializeField] private LayerMask m_interactLayer;

    private InputAction m_interact;
    private ContactFilter2D m_filter;

    void Awake()
    {
        m_inputs.Enable();

        m_interact = m_inputs.FindAction("Interact");
        m_filter.SetLayerMask(m_interactLayer);
    }

    void Update()
    {
        if (m_interact.WasPressedThisFrame())
        {
            var collisions = new Collider2D[1];

            m_interactArea.OverlapCollider(m_filter, collisions);

            foreach (Collider2D collision in collisions)
            {
                if (collision)
                {
                    if (collision.TryGetComponent<IInteract>(out var interact))
                    {
                        interact.Interact();
                    }
                }
            }
        }
    }
}