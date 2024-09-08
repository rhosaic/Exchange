using UnityEngine;

/// <summary>
/// Intercept and deactive HitBoxes
/// </summary>
public class BlockBox : HitBox
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

    /// <summary>
    /// Intercept HitBoxes and heal Composure
    /// </summary>
    /// <param name="collision"></param>
    void BlockAttack(Collider2D collision)
    {
        if (collision)
        {
            if (collision.TryGetComponent<HitBox>(out var hitbox))
            {
                if (hitbox.IsActive)
                {
                    m_statusDisplay.Status.Composure.HealCapped(hitbox.Damage / 2);
                    hitbox.IsActive = false;
                }
            }
        }
    }
}