using UnityEngine;
using UnityEngine.UIElements;

public class StrikeManager : MonoBehaviour
{
    public bool IsStartStrike1 { get => m_isStartStrike1; set { m_isStartStrike1 = value; } }
    public bool IsStartStrike2 { get => m_isStartStrike2; set { m_isStartStrike2 = value; } }

    [SerializeField] private GameObject m_moveHorizontalObject;
    [SerializeField] private Transform m_playerPosition;
    [SerializeField] private Transform m_rightStrikePosition;
    [SerializeField] private Transform m_leftStrikePosition;
    [SerializeField] private GameObject m_strike1AttackObject;
    [SerializeField] private GameObject m_strike2AttackObject;

    private MoveHorizontal m_moveHorizontal;
    private Vector3 m_strike1DefaultPosition;
    private Vector3 m_strike2DefaultPosition;
    private Attack m_strike1Attack;
    private Attack m_strike2Attack;
    private bool m_isStartStrike1;
    private bool m_isStartStrike2;
    private bool m_isStrike1DamageSent;
    private bool m_isStrike2DamageSent;

    void Awake()
    {
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_strike1Attack = m_strike1AttackObject.GetComponent<Attack>();
        m_strike2Attack = m_strike2AttackObject.GetComponent<Attack>();

        m_strike1DefaultPosition = m_strike1AttackObject.transform.position;
        m_strike2DefaultPosition = m_strike2AttackObject.transform.position;

        m_isStartStrike1 = false;
        m_isStartStrike2 = false;
    }

    void Update()
    {
        var strikePosition = m_rightStrikePosition;

        if (m_moveHorizontal.Direction == MoveHorizontal.MoveDirection.Left)
        {
            strikePosition = m_leftStrikePosition;
        }

        if (m_isStartStrike1)
        {
            if (!m_isStrike1DamageSent)
            {
                m_isStrike1DamageSent = true;
                m_strike1Attack.IsDamage = true;
            }

            m_strike1AttackObject.transform.position = strikePosition.position;
        }
        else
        {
            m_isStrike1DamageSent = false;
            m_strike1AttackObject.transform.position = m_strike1DefaultPosition;
        }

        if (m_isStartStrike2)
        {
            if (!m_isStrike2DamageSent)
            {
                m_isStrike2DamageSent = true;
                m_strike2Attack.IsDamage = true;
            }

            m_strike2AttackObject.transform.position = strikePosition.position;
        }
        else
        {
            m_isStrike2DamageSent = false;
            m_strike2AttackObject.transform.position = m_strike2DefaultPosition;
        }
    }
}