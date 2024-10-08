using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] InputActionAsset m_inputs;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_statusDisplayObject;
    [SerializeField] GameObject m_hurtBoxObject;
    [SerializeField] Rigidbody2D m_body;
    [SerializeField] float m_dashMaximumTime;
    [SerializeField] float m_dashSpeed;
    [SerializeField] float m_dashCost;

    InputAction m_dash;
    MoveHorizontal m_moveHorizontal;
    FormManager m_formManager;
    StatusDisplay m_statusDisplay;
    HurtBox m_hurtBox;
    float m_dashTime;
    bool m_isDash;
    bool m_isDashPaid;
    bool m_isInvicibleReset;

    void Awake()
    {
        m_inputs.Enable();
        m_dash = m_inputs.FindAction("Dash");

        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_statusDisplay = m_statusDisplayObject.GetComponent<StatusDisplay>();
        m_hurtBox = m_hurtBoxObject.GetComponent<HurtBox>();

        m_dashTime = 0.0f;
        m_isDash = false;
        m_isDashPaid = false;
        m_isInvicibleReset = true;
    }

    void Update()
    {
        if (m_formManager.CurrentForm == Form.Two)
        {
            if (m_dash.WasPressedThisFrame() && !m_isDash)
            {
                m_isDash = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (m_isDash)
        {
            m_hurtBox.IsInvincible = true;
            m_isInvicibleReset = false;

            DashMove();
        }
        else
        {
            if (!m_isInvicibleReset)
            {
                m_hurtBox.IsInvincible = false;
                m_isInvicibleReset = true;
            }
        }
    }

    private void DashMove()
    {
        m_dashTime += Time.fixedDeltaTime;

        if (m_statusDisplay.Status.Composure.Current >= m_dashCost)
        {
            if (!m_isDashPaid)
            {
                m_statusDisplay.Status.Composure.DamageFloored(m_dashCost);
                m_isDashPaid = true;
            }
        }

        if (m_isDashPaid)
        {
            if (m_dashTime < m_dashMaximumTime)
            {
                var position = m_body.transform.position;

                if (m_moveHorizontal.Direction == MoveDirection.Left)
                {
                    position += (Time.fixedDeltaTime * m_dashSpeed * Vector3.left);
                }
                else if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    position += (Time.fixedDeltaTime * m_dashSpeed * Vector3.right);
                }

                m_body.transform.position = position;
            }
            else
            {
                m_isDash = false;
                m_isDashPaid = false;
                m_dashTime = 0.0f;
            }
        }
    }
}