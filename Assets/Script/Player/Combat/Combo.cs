using UnityEngine;
using UnityEngine.InputSystem;

public class Combo : MonoBehaviour
{
    public bool IsIdle { get => m_isIdle; private set { } }
    public int ComboIndex { get => m_comboIndex; private set { } }

    private const float RESET_STRIKE1 = 0.55f;
    private const float RESET_STRIKE2 = 0.55f;
    private const float RESET_MARK = 1.1f;
    private const float DELAY_STRIKE1 = 0.35f;
    private const float DELAY_STRIKE2 = 0.35f;
    private const float DELAY_MARK = 0.9f;
    private const int COMBO_MAXIMUM_INDEX = 2;

    private readonly float[] m_inputDelays =
        { DELAY_STRIKE1, DELAY_STRIKE2, DELAY_MARK };
    private readonly float[] m_resets =
        { RESET_STRIKE1, RESET_STRIKE2, RESET_MARK };

    [SerializeField] private InputActionAsset m_inputs;
    [SerializeField] private GameObject m_formManagerObject;

    private InputAction m_strike;
    private FormManager m_formManager;
    private int m_comboIndex;
    private float m_timeSinceInput;
    private bool m_isIdle;

    void Awake()
    {
        m_inputs.Enable();
        m_strike = m_inputs.FindAction("Strike");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();

        m_comboIndex = 0;
        m_timeSinceInput = 0.0f;
        m_isIdle = true;
    }

    void Update()
    {
        if (m_formManager.CurrentForm == Form.Two)
        {
            if (m_isIdle)
            {
                if (m_strike.WasPressedThisFrame())
                {
                    m_comboIndex = 0;
                    m_isIdle = false;
                }
            }

            if (!m_isIdle)
            {
                UpdateComboIndex();
            }
        }
        else
        {
            m_comboIndex = 0;
            m_timeSinceInput = 0.0f;
            m_isIdle = true;
        }
    }

    private void UpdateComboIndex()
    {
        m_timeSinceInput += Time.deltaTime;

        if (m_timeSinceInput < m_resets[m_comboIndex])
        {

            if (
                m_strike.WasPressedThisFrame()
                && m_timeSinceInput > m_inputDelays[m_comboIndex])
            {
                if (m_comboIndex == COMBO_MAXIMUM_INDEX)
                {
                    m_comboIndex = 0;
                }
                else
                {
                    ++m_comboIndex;
                }

                m_timeSinceInput = 0.0f;
            }
        }
        else
        {
            m_isIdle = true;
            m_comboIndex = 0;

            m_timeSinceInput = 0.0f;
        }
    }
}