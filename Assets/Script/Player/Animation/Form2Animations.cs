using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Play Form 2 animaitons
/// </summary>
public class Form2Animations : MonoBehaviour
{
    const string LEFT_PASSIVE = "Form2LeftPassive";
    const string LEFT_STRIKE1 = "Form2LeftStrike1";
    const string LEFT_STRIKE2 = "Form2LeftStrike2";
    const string LEFT_MARK = "Form2LeftMark";
    const string RIGHT_PASSIVE = "Form2RightPassive";
    const string RIGHT_STRIKE1 = "Form2RightStrike1";
    const string RIGHT_STRIKE2 = "Form2RightStrike2";
    const string RIGHT_MARK = "Form2RightMark";

    readonly string[] m_leftComboAnimations =
       { LEFT_STRIKE1, LEFT_STRIKE2, LEFT_MARK };
    readonly string[] m_rightComboAnimations =
       { RIGHT_STRIKE1, RIGHT_STRIKE2, RIGHT_MARK };

    [SerializeField] InputActionAsset m_inputs;
    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_animatorManagerObject;
    [SerializeField] GameObject m_comboObject;

    FormManager m_formManager;
    MoveHorizontal m_moveHorizontal;
    AnimatorManager m_animatorManager;
    Combo m_combo;

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
        if (m_formManager.CurrentForm == Form.Two)
        {
            var state = LEFT_PASSIVE;
            var comboAnimations = m_leftComboAnimations;

            //  Play Form 2 combo animation
            if (!m_combo.IsIdle)
            {
                if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    comboAnimations = m_rightComboAnimations;
                }

                state = comboAnimations[m_combo.ComboIndex];
            }
            //  Or, play Form 2 passive animation
            else
            {
                if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    state = RIGHT_PASSIVE;
                }
            }

            m_animatorManager.PlayState(state);
        }
    }
}