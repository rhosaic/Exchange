using UnityEngine;

public class MoveVertical : MonoBehaviour
{
    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private Rigidbody2D m_body;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_timeToApex;

    private FormManager m_formManager;
    private float m_form3Time;
    private float m_currentForm3Y;
    private float m_previousGravityScale;

    void Awake()
    {
        m_form3Time = 0.0f;
        m_previousGravityScale = m_body.gravityScale;

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
    }

    void FixedUpdate()
    {
        if (m_formManager.CurrentForm == FormManager.Form.Three)
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

    private void Ascend()
    {
        var position = m_body.transform.position;

        m_form3Time += Time.fixedDeltaTime;

        if ((m_form3Time < m_timeToApex) && m_formManager.IsForm3Ascending)
        {
            position.y += (Time.fixedDeltaTime * m_speed);

            m_currentForm3Y = position.y;
        }

        position.y = m_currentForm3Y;

        m_body.transform.position = position;
    }
}