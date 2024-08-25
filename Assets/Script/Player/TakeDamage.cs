using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private GameObject m_statusDisplayObject;
    [SerializeField] private BoxCollider2D m_playerBody;
    [SerializeField] private LayerMask m_enemyAttackLayer;

    private StatusDisplay m_statusDisplay;
    private Collider2D[] m_collisions;
    private ContactFilter2D m_filter;

    void Awake()
    {
        m_statusDisplay = m_statusDisplayObject.GetComponent<StatusDisplay>();
        m_collisions = new Collider2D[2];
        m_filter.layerMask = m_enemyAttackLayer;
    }

    void Update()
    {
        m_playerBody.OverlapCollider(m_filter, m_collisions);

        foreach (Collider2D collision in m_collisions)
        {
            if (collision)
            {
                var attack = collision.GetComponentInParent<Attack>();

                if (attack)
                {
                    if (attack.IsDamage)
                    {
                        m_statusDisplay.Status.Damage(attack.Damage);
                    }

                    attack.IsDamage = false;
                }
            }
        }
    }
}