using System;
using UnityEngine;

public enum DamageType { Standard, Composure, Status };

public class HitBox : MonoBehaviour
{
    public float Damage { get => m_damageAmount; private set { } }
    public bool IsActive { get => m_isActive; set { m_isActive = value; } }
    public BoxCollider2D Collider { get => m_collider; private set { } }

    [SerializeField] protected float m_damageAmount;
    [SerializeField] protected DamageType m_damageType;
    [SerializeField] protected LayerMask m_searchLayer;
    [SerializeField] BoxCollider2D m_collider;

    protected ContactFilter2D m_filter;
    bool m_isActive;

    void Awake()
    {
        m_filter.SetLayerMask(m_searchLayer);

        IsActive = false;
    }

    void Update()
    {
        if (IsActive)
        {
            HitboxProcess(DealDamage);
        }
    }

    protected void HitboxProcess(Action<Collider2D> collisionEffect)
    {
        var collisions = new Collider2D[1];
        m_collider.OverlapCollider(m_filter, collisions);

        foreach (Collider2D collision in collisions)
        {
            collisionEffect(collision);
        }
    }

    void DealDamage(Collider2D collision)
    {
        if (collision)
        {
            if (collision.TryGetComponent<HurtBox>(out var hurtbox))
            {
                ChooseDamage(hurtbox);
            }
        }
    }

    void ChooseDamage(HurtBox hurtbox)
    {
        if (!hurtbox.IsInvincible)
        {
            if (m_damageType == DamageType.Standard)
            {
                hurtbox.StatusDisplay.Status.StandardDamage(m_damageAmount);
            }
            else if (m_damageType == DamageType.Composure)
            {
                hurtbox.StatusDisplay.Status.Composure.DamageFloored(m_damageAmount);
            }
            else if (m_damageType == DamageType.Status)
            {
                hurtbox.StatusDisplay.Status.StatusDamage(m_damageAmount);
            }
        }

        IsActive = false;
    }
}