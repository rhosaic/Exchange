using UnityEngine;
using UnityEngine.InputSystem;

public class Form2Animations : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_inputs;
    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_moveHorizontalObject;
    [SerializeField] private GameObject m_animatorManagerObject;
    [SerializeField] private GameObject m_strikeManagerObject;

    private const float RESET_STRIKE1 = 0.6f;
    private const float RESET_STRIKE2 = 0.6f;
    private const float RESET_MARK = 1.05f;
    private const float DELAY_STRIKE1 = 0.35f;
    private const float DELAY_STRIKE2 = 0.37f;
    private const float DELAY_MARK = 0.85f;
    private const int STRIKE_MAXIMUM = 3;

    private const string LEFT_PASSIVE = "Form2LeftPassive";
    private const string LEFT_STRIKE1 = "Form2LeftStrike1";
    private const string LEFT_STRIKE2 = "Form2LeftStrike2";
    private const string LEFT_MARK = "Form2LeftMark";
    private const string RIGHT_PASSIVE = "Form2RightPassive";
    private const string RIGHT_STRIKE1 = "Form2RightStrike1";
    private const string RIGHT_STRIKE2 = "Form2RightStrike2";
    private const string RIGHT_MARK = "Form2RightMark";

    private readonly string[] m_leftCombo = { LEFT_STRIKE1, LEFT_STRIKE2, LEFT_MARK };
    private readonly string[] m_rightCombo = { RIGHT_STRIKE1, RIGHT_STRIKE2, RIGHT_MARK };
    private readonly float[] m_delays = { DELAY_STRIKE1, DELAY_STRIKE2, DELAY_MARK };
    private readonly float[] m_resets = { RESET_STRIKE1, RESET_STRIKE2, RESET_MARK };
    private InputAction m_strike;
    private FormManager m_formManager;
    private MoveHorizontal m_moveHorizontal;
    private AnimatorManager m_animatorManager;
    private StrikeManager m_strikeManager;

    private int m_strikeCount;
    private float m_strikeTime;
    private bool m_isStart;

    void Awake()
    {
        m_inputs.Enable();

        m_strike = m_inputs.FindAction("Strike");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_animatorManager = m_animatorManagerObject.GetComponent<AnimatorManager>();
        m_strikeManager = m_strikeManagerObject.GetComponent<StrikeManager>();

        m_strikeCount = 0;
        m_strikeTime = 0.0f;
        m_isStart = false;
    }

    void Update()
    {
        m_strikeManager.IsStartStrike1 = false;
        m_strikeManager.IsStartStrike2 = false;

        if (m_formManager.CurrentForm == FormManager.Form.Two)
        {
            var state = LEFT_PASSIVE;
            var comboNames = m_leftCombo;

            if (
                m_moveHorizontal.Direction
                == MoveHorizontal.MoveDirection.Right)
            {
                state = RIGHT_PASSIVE;
                comboNames = m_rightCombo;
            }

            if (!m_isStart)
            {
                if (m_strike.WasPressedThisFrame())
                {
                    m_isStart = true;
                }
            }
            else
            {
                m_strikeTime += Time.deltaTime;

                if (m_strikeTime > m_resets[m_strikeCount])
                {
                    m_isStart = false;
                    m_strikeTime = 0.0f;
                    m_strikeCount = 0;
                }
                else
                {
                    if (m_strikeCount == 0)
                    {
                        m_strikeManager.IsStartStrike1 = true;
                    }
                    else if (m_strikeCount == 1)
                    {
                        m_strikeManager.IsStartStrike2 = true;
                    }

                    if (
                        m_strike.WasPressedThisFrame()
                        && m_strikeTime > m_delays[m_strikeCount])
                    {
                        m_strikeTime = 0.0f;

                        if (m_strikeCount + 1 == STRIKE_MAXIMUM)
                        {
                            m_strikeCount = 0;
                        }
                        else
                        {
                            ++m_strikeCount;
                        }
                    }

                    state = comboNames[m_strikeCount];
                }
            }

            m_animatorManager.PlayState(state);
        }
        else
        {
            m_isStart = false;
            m_strikeTime = 0.0f;
            m_strikeCount = 0;
        }
    }
}