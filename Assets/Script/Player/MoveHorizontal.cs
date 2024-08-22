using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveHorizontal : MonoBehaviour
{
    public enum MoveDirection { Left, Right };
    [SerializeField] private InputActionAsset m_inputs;
    [SerializeField] private Rigidbody2D m_body;
    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_statusObject;
    [SerializeField] private float m_horizontalSpeed;
    [SerializeField] private float m_dashSpeed;
    [SerializeField] private float m_dashCost;
    [SerializeField] private float m_dashMaximumTime;
    private InputAction m_moveLeft;
    private InputAction m_moveRight;
    private InputAction m_dash;
    private FormManager m_formManager;
    private StatusDisplay m_status;
    private bool m_isDash;
    private float m_dashTime;
    private bool m_isDashPaid;
    public MoveDirection Direction { get; private set; }

    void Awake()
    {
        m_inputs.Enable();

        m_moveLeft = m_inputs.FindAction("MoveLeft");
        m_moveRight = m_inputs.FindAction("MoveRight");
        m_dash = m_inputs.FindAction("Dash");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_status = m_statusObject.GetComponent<StatusDisplay>();

        m_isDash = false;
        m_isDashPaid = false;
        m_dashTime = 0.0f;
    }

    void Update()
    {
        if (m_formManager.CurrentForm == FormManager.Form.Two)
        {
            if (m_dash.WasPressedThisFrame() && !m_isDash)
            {
                m_isDash = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (m_formManager.CurrentForm == FormManager.Form.Zero)
        {
            UpdateMoveHorizontal();
        }

        if (m_isDash)
        {
            Dash();
        }
    }
    private void UpdateMoveHorizontal()
    {
        var moveLeftValue = m_moveLeft.ReadValue<float>();
        var moveRightValue = m_moveRight.ReadValue<float>();

        var position = m_body.transform.position;

        if (moveLeftValue > float.Epsilon)
        {
            position += (Time.fixedDeltaTime * m_horizontalSpeed * Vector3.left);
            Direction = MoveDirection.Left;
        }
        else if (moveRightValue > float.Epsilon)
        {
            position += (Time.fixedDeltaTime * m_horizontalSpeed * Vector3.right);
            Direction = MoveDirection.Right;
        }

        m_body.transform.position = position;

        var rotation = m_body.transform.rotation;
        rotation.z = 0;

        m_body.transform.rotation = rotation;
    }

    private void Dash()
    {
        m_dashTime += Time.deltaTime;

        if (
            (m_status.Status.Composure.Current > m_dashCost)
            || (Math.Abs((m_status.Status.Composure.Current - m_dashCost))
                > float.Epsilon))
        {
            if (!m_isDashPaid)
            {
                m_status.Status.Composure.DamageFloored(m_dashCost);
                m_isDashPaid = true;
            }

            if (m_dashTime < m_dashMaximumTime)
            {
                Debug.Log("Dash time: " + m_dashTime);
                var position = m_body.transform.position;

                if (Direction == MoveDirection.Left)
                {
                    position += (Time.fixedDeltaTime * m_dashSpeed * Vector3.left);
                }
                else if (Direction == MoveDirection.Right)
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