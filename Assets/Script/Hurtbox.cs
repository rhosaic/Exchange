using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public bool IsMarked { get => m_isMarked; set { m_isMarked = value; } }
    public StatusDisplay StatusDisplay { get => m_status; private set { } }

    [SerializeField] private GameObject m_statusObject;

    private StatusDisplay m_status;
    private bool m_isMarked;

    void Awake()
    {
        m_status = m_statusObject.GetComponent<StatusDisplay>();
        m_isMarked = false;
    }
}