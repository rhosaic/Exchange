using UnityEngine;

public class Blockbox : Hitbox
{
    [SerializeField] GameObject m_statusDisplayObject;

    StatusDisplay m_statusDisplay;
    void Awake()
    {
        m_statusDisplay = m_statusDisplayObject.GetComponent<StatusDisplay>();

        IsActive = false;
    }

    void Update()
    {
        if (IsActive)
        {
            HitboxProcess(BlockAttack);
        }
    }

    void BlockAttack(Collider2D collision)
    {
        if (collision)
        {
            if (collision.TryGetComponent<Hitbox>(out var hitbox))
            {
                hitbox.IsActive = false;
                m_statusDisplay.Status.Composure.HealCapped(hitbox.Damage / 2);
            }
        }
    }
}