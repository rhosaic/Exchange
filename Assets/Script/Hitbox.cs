using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public StatusDisplay StatusDisplay { get => m_status; private set { } }

    [SerializeField] private GameObject m_statusObject;

    private StatusDisplay m_status;

    void Awake()
    {
        m_status = m_statusObject.GetComponent<StatusDisplay>();
    }
}