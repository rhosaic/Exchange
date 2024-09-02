using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveVertical : MonoBehaviour
{
    public float HeightRatio { get => m_heightRatio; private set { } }

    [SerializeField] InputActionAsset m_inputs;
    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] Rigidbody2D m_body;
    [SerializeField] float m_speed;
    [SerializeField] float m_timeToApex;

    InputAction m_form3;
    FormManager m_formManager;
    float m_form3Time;
    float m_previousGravityScale;
    float m_heightRatio;

    void Awake()
    {
        m_inputs.Enable();
        m_form3 = m_inputs.FindAction("EnterForm3");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();

        m_form3Time = 0.0f;
        m_previousGravityScale = m_body.gravityScale;
        m_heightRatio = 0.0f;
    }

    void FixedUpdate()
    {
        UpdateHeightRatio();

        if (m_formManager.CurrentForm == Form.Three)
        {
            m_body.gravityScale = 0.0f;

            Ascend();
        }
        else
        {
            m_body.gravityScale = m_previousGravityScale;
            m_form3Time = 0.0f;
        }
    }

    void Ascend()
    {
        var position = m_body.transform.position;

        m_form3Time += Time.fixedDeltaTime;

        if (m_form3Time < m_timeToApex && m_form3.IsPressed())
        {
            position.y += Time.fixedDeltaTime * m_speed;
        }

        m_body.transform.position = position;
    }

    void UpdateHeightRatio()
    {
        if (m_formManager.CurrentForm == Form.Three)
        {
            if (m_form3Time < m_timeToApex)
            {
                m_heightRatio = m_form3Time / m_timeToApex;
            }
            else
            {
                m_heightRatio = 1.0f;
            }
        }
        else
        {
            m_heightRatio = 0.0f;
        }
    }
}