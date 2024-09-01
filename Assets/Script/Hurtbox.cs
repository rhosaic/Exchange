using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public bool IsMarked { get => m_isMarked; set { m_isMarked = value; } }
    public bool IsInvincible { get => m_isInvincible; set { m_isInvincible = value; } }
    public StatusDisplay StatusDisplay { get => m_status; private set { } }

    [SerializeField] GameObject m_statusObject;

    StatusDisplay m_status;
    bool m_isInvincible;
    bool m_isMarked;

    void Awake()
    {
        m_status = m_statusObject.GetComponent<StatusDisplay>();
        m_isInvincible = false;
        m_isMarked = false;
    }
}