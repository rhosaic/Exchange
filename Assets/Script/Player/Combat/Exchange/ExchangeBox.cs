using System.Collections.Generic;
using UnityEngine;

public class ExchangeBox : HitBox
{
    const float HEIGHT_DAMAGE_FACTOR = 0.75f;
    const float TRAVEL_TIME_DAMAGE_FACTOR = 0.25f;
    const float TRAVEL_TIME_MAXIMUMDAMAGE = 0.37f;

    [SerializeField] GameObject m_moveExchangeObject;
    [SerializeField] GameObject m_moveVerticalObject;

    MoveExchange m_moveExchange;
    MoveVertical m_moveVertical;
    List<HurtBox> m_damagedHurtBoxes;

    void Awake()
    {
        m_moveExchange = m_moveExchangeObject.GetComponent<MoveExchange>();
        m_moveVertical = m_moveVerticalObject.GetComponent<MoveVertical>();

        m_filter.SetLayerMask(m_searchLayer);
        m_damagedHurtBoxes = new List<HurtBox>();

        IsActive = false;
    }

    void Update()
    {
        if (IsActive)
        {
            HitboxProcess(Exchange);
        }
        else
        {
            m_damagedHurtBoxes = new List<HurtBox>();
        }
    }

    void Exchange(Collider2D collision)
    {
        if (collision)
        {
            if (collision.TryGetComponent<MarkBody>(out var body))
            {
                DamageBody(body);
            }
        }
    }

    void DamageBody(MarkBody body)
    {
        var endOffset = 0;
        var isAlreadyDamaged = false;
        var hurtBox = body.HurtBox;

        if (hurtBox.IsMarked)
        {
            hurtBox.IsMarked = false;
        }

        while (endOffset < m_damagedHurtBoxes.Count && !isAlreadyDamaged)
        {
            var compared =
                m_damagedHurtBoxes[m_damagedHurtBoxes.Count - endOffset - 1];

            if (hurtBox == compared)
            {
                isAlreadyDamaged = true;
            }
        }

        if (!isAlreadyDamaged)
        {
            var heightDamageWeight =
                HEIGHT_DAMAGE_FACTOR * m_moveVertical.HeightRatio;
            var travelTimeDamageWeight =
                TRAVEL_TIME_DAMAGE_FACTOR
                    * (m_moveExchange.TimeToTarget / TRAVEL_TIME_MAXIMUMDAMAGE);

            var damage =
                m_damageAmount * (heightDamageWeight + travelTimeDamageWeight);

            hurtBox.StatusDisplay.Status.StatusDamage(damage);
            m_damagedHurtBoxes.Add(hurtBox);
        }

        if (body == m_moveExchange.Target)
        {
            IsActive = false;
        }
    }
}