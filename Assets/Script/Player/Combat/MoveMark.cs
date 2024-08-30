using UnityEngine;
using UnityEngine.Rendering;

public class MoveMark : MonoBehaviour
{
    public bool IsMove { get => m_isMove; set { m_isMove = value; } }

    [SerializeField] private float m_arrowSpawnDelay;
    [SerializeField] private float m_travelTimeMaximum;
    [SerializeField] private float m_speed;
    [SerializeField] private GameObject m_markObject;
    [SerializeField] private GameObject m_moveHorizontalObject;

    private MoveHorizontal m_moveHorizontal;
    private float m_time;
    private bool m_isMove;

    void Awake()
    {
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();

        m_time = 0.0f;
        m_isMove = false;
    }

    void FixedUpdate()
    {
        if (m_isMove)
        {
            SendArrow();
        }
        else
        {
            m_time = 0.0f;
        }
    }

    void SendArrow()
    {
        m_time += Time.fixedDeltaTime;

        if (m_time < m_travelTimeMaximum)
        {
            var position = m_markObject.transform.position;
            var directionFactor = 1;

            if (m_moveHorizontal.Direction == MoveHorizontal.MoveDirection.Left)
            {
                directionFactor = -1;
            }

            var step = Time.fixedDeltaTime * m_speed * directionFactor;

            position.x += step;

            m_markObject.transform.position = position;
        }
    }
}