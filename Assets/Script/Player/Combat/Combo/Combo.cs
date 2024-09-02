using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Update a combo index from timed user input
/// </summary>
public class Combo : MonoBehaviour
{
    //  Whether or not the player is in a combo
    public bool IsIdle { get => m_isIdle; set { } }
    public int ComboIndex { get => m_comboIndex; set { } }

    //  Time above which combo will reset
    const float RESET_STRIKE1 = 0.55f;
    const float RESET_STRIKE2 = 0.55f;
    const float RESET_MARK = 1.1f;
    //  Time above which the next combo move will begin
    const float DELAY_STRIKE1 = 0.35f;
    const float DELAY_STRIKE2 = 0.35f;
    const float DELAY_MARK = 0.9f;
    const int COMBO_MAXIMUM_INDEX = 2;

    readonly float[] m_inputDelays =
       { DELAY_STRIKE1, DELAY_STRIKE2, DELAY_MARK };
    readonly float[] m_resets =
       { RESET_STRIKE1, RESET_STRIKE2, RESET_MARK };

    [SerializeField] InputActionAsset m_inputs;
    [SerializeField] GameObject m_formManagerObject;

    InputAction m_strike;
    FormManager m_formManager;
    int m_comboIndex;
    float m_timeSinceInput;
    bool m_isIdle;

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

    /// <summary>
    /// Read timed user input
    /// </summary>
    void UpdateComboIndex()
    {
        m_timeSinceInput += Time.deltaTime;

        //  Combo must still be going
        if (m_timeSinceInput < m_resets[m_comboIndex])
        {

            //  Player must active next move after a delay
            if (
                m_strike.WasPressedThisFrame()
                && m_timeSinceInput > m_inputDelays[m_comboIndex])
            {
                //  Reset to combo beginning
                if (m_comboIndex == COMBO_MAXIMUM_INDEX)
                {
                    m_comboIndex = 0;
                }
                //  Or, begin next move
                else
                {
                    ++m_comboIndex;
                }

                m_timeSinceInput = 0.0f;
            }
        }
        //  0r, combo is over
        else
        {
            m_isIdle = true;
            m_comboIndex = 0;

            m_timeSinceInput = 0.0f;
        }
    }
}