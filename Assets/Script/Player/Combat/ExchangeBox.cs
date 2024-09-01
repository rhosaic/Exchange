using System.Collections.Generic;
using UnityEngine;

public class ExchangeBox : HitBox
{
    [SerializeField] GameObject m_moveExchangeObject;

    MoveExchange m_moveExchange;
    List<HurtBox> m_damagedHurtBoxes;

    void Awake()
    {
        m_moveExchange = m_moveExchangeObject.GetComponent<MoveExchange>();

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
            hurtBox.StatusDisplay.Status.StatusDamage(m_damageAmount);
            m_damagedHurtBoxes.Add(hurtBox);
        }

        if (body == m_moveExchange.Target)
        {
            IsActive = false;
        }
    }
}