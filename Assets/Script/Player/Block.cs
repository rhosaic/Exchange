using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsActive { get => m_isActive; set { m_isActive = value; } }

    [SerializeField] private GameObject m_moveHorizontalObject;
    [SerializeField] private GameObject m_statusDisplayObject;
    [SerializeField] private Transform m_playerPosition;
    [SerializeField] private Vector3 m_rightBlockOffset;
    [SerializeField] private Vector2 m_size;
    [SerializeField] private LayerMask m_enemyAttackLayer;

    private MoveHorizontal m_moveHorizontal;
    private StatusDisplay m_statusDisplay;

    private Vector3 m_leftBlockOffset;
    private bool m_isActive;

    void Awake()
    {
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_statusDisplay = m_statusDisplayObject.GetComponent<StatusDisplay>();

        m_leftBlockOffset = m_rightBlockOffset;
        m_leftBlockOffset.x *= -1;

        m_isActive = false;
    }

    void Update()
    {
        if (m_isActive)
        {
            var blockPosition = m_playerPosition.transform.position + m_rightBlockOffset;

            if (m_moveHorizontal.Direction == MoveHorizontal.MoveDirection.Left)
            {
                blockPosition = m_playerPosition.transform.position + m_leftBlockOffset;
            }

            var collider = Physics2D.OverlapBox(blockPosition, m_size, 0.0f, m_enemyAttackLayer);

            if (collider)
            {
                var attack = collider.GetComponentInChildren<Attack>();

                if (attack)
                {
                    if (attack.IsDamage)
                    {
                        m_statusDisplay.Status.Composure.HealCapped(attack.Damage / 2);
                    }

                    attack.IsDamage = false;
                }
            }
        }
    }
}