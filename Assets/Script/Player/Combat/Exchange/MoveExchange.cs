using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveExchange : MonoBehaviour
{
    public MarkBody Target { get => m_target; private set { } }

    [SerializeField] InputActionAsset m_inputs;
    [SerializeField] Rigidbody2D m_playerBody;
    [SerializeField] BoxCollider2D m_playerCollider;
    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_exchangeBoxObject;
    [SerializeField] float m_speed;
    [SerializeField] HurtBox m_playerHurtBox;

    InputAction m_exchange;
    FormManager m_formManager;
    MoveHorizontal m_moveHorizontal;
    ExchangeBox m_exchangeBox;
    List<MarkBody> m_marks;
    MarkBody m_target;
    bool m_isExchange;
    bool m_isInvicibleReset;

    void Awake()
    {
        m_inputs.Enable();
        m_exchange = m_inputs.FindAction("Interact");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_moveHorizontal =
            m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_exchangeBox = m_exchangeBoxObject.GetComponent<ExchangeBox>();

        m_marks = new List<MarkBody>();

        m_target = null;
        m_isExchange = false;
        m_isInvicibleReset = true;
    }

    void Update()
    {
        ClearUnmarked();

        if (m_marks.Count != 0)
        {
            m_target = m_marks.Last();
        }
        else
        {
            m_target = null;
        }

        if (m_formManager.CurrentForm == Form.Three)
        {
            if (m_exchange.WasPressedThisFrame())
            {
                m_isExchange = true;
            }
        }
        else
        {
            m_isExchange = false;
            m_target = null;
        }
    }

    void FixedUpdate()
    {
        if (m_isExchange && m_target)
        {
            ExchangeIfMarked();
        }
        else
        {
            m_exchangeBox.IsActive = false;
        }
    }

    public void RegisterMarkBody(MarkBody markBody)
    {
        m_marks.Add(markBody);
    }

    void ExchangeIfMarked()
    {
        if (m_marks.Count != 0)
        {
            var isFacingTargetRight =
                m_moveHorizontal.Direction == MoveDirection.Right
                    && m_target.transform.position.x
                        > m_playerBody.transform.position.x;
            var isFacingTargetLeft =
                m_moveHorizontal.Direction == MoveDirection.Left
                    && m_target.transform.position.x
                        < m_playerBody.transform.position.x;

            var isFacingTarget = isFacingTargetLeft || isFacingTargetRight;


            if (isFacingTarget)
            {
                m_exchangeBox.IsActive = true;
                m_playerHurtBox.IsInvincible = true;
                m_isInvicibleReset = false;
            }
            else
            {
                if (!m_isInvicibleReset)
                {
                    m_playerHurtBox.IsInvincible = false;
                    m_isInvicibleReset = true;
                }

                m_exchangeBox.IsActive = false;
            }

            Exchange(isFacingTarget, m_exchangeBox.IsActive);
        }
        else
        {
            if (!m_isInvicibleReset)
            {
                m_playerHurtBox.IsInvincible = false;
                m_isInvicibleReset = true;
            }

            m_exchangeBox.IsActive = false;
            m_isExchange = false;
            m_target = null;
        }
    }

    void Exchange(bool isFacingTarget, bool isActive)
    {
        if (isFacingTarget && isActive)
        {
            m_exchangeBox.IsActive = true;

            MoveToTarget();
        }
        else
        {
            m_exchangeBox.IsActive = false;
            m_isExchange = false;
            m_target = null;
        }
    }

    void MoveToTarget()
    {
        var position = m_playerCollider.bounds.center;
        var targetCenter = m_target.Collider.bounds.center;

        var direction = (targetCenter - position).normalized;

        var step = Time.fixedDeltaTime * m_speed * direction;

        position += step;

        m_playerBody.transform.position = position;
    }

    void ClearUnmarked()
    {
        for (var endOffset = 0; endOffset < m_marks.Count; ++endOffset)
        {
            var index = m_marks.Count - endOffset - 1;

            if (!m_marks[index].HurtBox.IsMarked)
            {
                m_marks.RemoveAt(index);
            }
        }
    }
}