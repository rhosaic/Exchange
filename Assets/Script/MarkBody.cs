using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MarkBody : MonoBehaviour
{
    const float MARK_TIME_MAXIMUM = 4.0f;

    public BoxCollider2D Collider { get => m_collider; set { } }
    public HurtBox HurtBox { get => m_hurtBox; set { } }

    [SerializeField] GameObject m_hurtboxObject;
    [SerializeField] GameObject m_markSymbolObject;
    [SerializeField] GameObject m_moveExchangeObject;
    [SerializeField] BoxCollider2D m_collider;

    HurtBox m_hurtBox;
    MoveExchange m_moveExchange;
    float m_markTime;
    bool m_isRegistered;
    GameObject m_markSymbol;

    void Awake()
    {
        m_hurtBox = m_hurtboxObject.GetComponent<HurtBox>();
        m_moveExchange = m_moveExchangeObject.GetComponent<MoveExchange>();

        m_markTime = 0.0f;
        m_isRegistered = false;
        m_markSymbol = null;
    }

    void Update()
    {
        if (m_hurtBox.IsMarked)
        {
            if (!m_isRegistered)
            {
                var spawnPosition = m_collider.bounds.center;
                spawnPosition.z = 0;

                m_markSymbol =
                    Instantiate(m_markSymbolObject, spawnPosition,
                        Quaternion.identity);
                m_moveExchange.RegisterMarkBody(this);
                m_isRegistered = true;
            }

            m_markTime += Time.deltaTime;

            if (m_markTime > MARK_TIME_MAXIMUM)
            {
                m_hurtBox.IsMarked = false;

                if (m_markSymbol)
                {
                    Destroy(m_markSymbol);
                }
            }
        }
        else
        {

            if (m_markSymbol)
            {
                Destroy(m_markSymbol);
            }

            m_hurtBox.IsMarked = false;
            m_markTime = 0.0f;
            m_isRegistered = false;
        }
    }
}