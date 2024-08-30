using UnityEngine;
using UnityEngine.InputSystem;

public class Form2Animations : MonoBehaviour
{

    private const string LEFT_PASSIVE = "Form2LeftPassive";
    private const string LEFT_STRIKE1 = "Form2LeftStrike1";
    private const string LEFT_STRIKE2 = "Form2LeftStrike2";
    private const string LEFT_MARK = "Form2LeftMark";
    private const string RIGHT_PASSIVE = "Form2RightPassive";
    private const string RIGHT_STRIKE1 = "Form2RightStrike1";
    private const string RIGHT_STRIKE2 = "Form2RightStrike2";
    private const string RIGHT_MARK = "Form2RightMark";

    private readonly string[] m_leftComboAnimations =
        { LEFT_STRIKE1, LEFT_STRIKE2, LEFT_MARK };
    private readonly string[] m_rightComboAnimations =
        { RIGHT_STRIKE1, RIGHT_STRIKE2, RIGHT_MARK };

    [SerializeField] private InputActionAsset m_inputs;
    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_moveHorizontalObject;
    [SerializeField] private GameObject m_animatorManagerObject;
    [SerializeField] private GameObject m_comboObject;

    private FormManager m_formManager;
    private MoveHorizontal m_moveHorizontal;
    private AnimatorManager m_animatorManager;
    private Combo m_combo;

    void Awake()
    {
        m_inputs.Enable();

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_animatorManager = m_animatorManagerObject.GetComponent<AnimatorManager>();
        m_combo = m_comboObject.GetComponent<Combo>();
    }

    void Update()
    {
        if (m_formManager.CurrentForm == FormManager.Form.Two)
        {
            var state = LEFT_PASSIVE;
            var comboAnimations = m_leftComboAnimations;

            if (!m_combo.IsIdle)
            {
                if (m_moveHorizontal.Direction
                        == MoveHorizontal.MoveDirection.Right)
                {
                    comboAnimations = m_rightComboAnimations;
                }

                state = comboAnimations[m_combo.ComboIndex];
            }
            else
            {
                if (
                    m_moveHorizontal.Direction
                        == MoveHorizontal.MoveDirection.Right)
                {
                    state = RIGHT_PASSIVE;
                }
            }

            m_animatorManager.PlayState(state);
        }
    }
}