using System;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private GameObject m_statusDisplayObject;
    [SerializeField] private BoxCollider2D m_body;
    [SerializeField] private LayerMask m_attackLayer;

    private StatusDisplay m_statusDisplay;
    private Collider2D[] m_collisions;
    private ContactFilter2D m_filter;

    void Awake()
    {
        m_statusDisplay = m_statusDisplayObject.GetComponent<StatusDisplay>();
        m_collisions = new Collider2D[1];
        m_filter.SetLayerMask(m_attackLayer);
    }

    void Update()
    {
        m_body.OverlapCollider(m_filter, m_collisions);

        foreach (Collider2D collision in m_collisions)
        {
            ApplyDamageIfAttack(collision);
        }
    }

    private void ApplyDamageIfAttack(Collider2D collider)
    {
        if (collider)
        {
            var attack = collider.GetComponent<Attack>();

            if (attack)
            {
                if (attack.IsDamage)
                {
                    if (attack.DamageType == DamageType.Standard)
                    {
                        m_statusDisplay.Status.Damage(attack.Damage);
                    }

                    if (attack.DamageType == DamageType.Composure)
                    {
                        //Debug.Log("Applying damage from: " + collider.name);
                        m_statusDisplay.Status.Composure.DamageFloored(attack.Damage);
                    }
                }

                attack.IsDamage = false;
            }
        }
    }
}