using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MarkBody : MonoBehaviour
{
    const float MARK_TIME_MAXIMUM = 4.0f;

    public BoxCollider2D Collider { get => m_collider; private set { } }
    public HurtBox HurtBox { get => m_hurtBox; private set { } }

    [SerializeField] GameObject m_hurtboxObject;
    [SerializeField] GameObject m_markSymbolObject;
    [SerializeField] GameObject m_moveExchangeObject;
    [SerializeField] BoxCollider2D m_collider;

    Vector3 m_markSymbolDefaultPosition;
    HurtBox m_hurtBox;
    MoveExchange m_moveExchange;
    float m_markTime;
    bool m_isRegistered;

    void Awake()
    {
        m_hurtBox = m_hurtboxObject.GetComponent<HurtBox>();
        m_moveExchange = m_moveExchangeObject.GetComponent<MoveExchange>();

        m_markSymbolDefaultPosition = m_markSymbolObject.transform.position;

        m_markTime = 0.0f;
        m_isRegistered = false;
    }

    void Update()
    {
        if (m_hurtBox.IsMarked)
        {
            if (!m_isRegistered)
            {
                m_moveExchange.RegisterMarkBody(this);
                m_isRegistered = true;
            }

            m_markTime += Time.deltaTime;

            if (m_markTime > MARK_TIME_MAXIMUM)
            {
                m_hurtBox.IsMarked = false;
            }

            var position = m_collider.bounds.center;
            position.z = 0;

            m_markSymbolObject.transform.position = position;
        }
        else
        {
            m_markSymbolObject.transform.position = m_markSymbolDefaultPosition;
            m_hurtBox.IsMarked = false;
            m_markTime = 0.0f;
            m_isRegistered = false;
        }
    }
}