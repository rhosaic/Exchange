using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Play Form 3 animations
/// </summary>
public class Form3Animations : MonoBehaviour
{
    const string LEFT_ACTIVE = "Form3LeftActive";
    const string LEFT_PASSIVE = "Form3LeftPassive";
    const string RIGHT_ACTIVE = "Form3RightActive";
    const string RIGHT_PASSIVE = "Form3RightPassive";

    [SerializeField] InputActionAsset m_inputs;
    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_animatorManagerObject;

    InputAction m_exchange;
    FormManager m_formManager;
    MoveHorizontal m_moveHorizontal;
    AnimatorManager m_animatorManager;

    void Awake()
    {
        m_inputs.Enable();
        m_exchange = m_inputs.FindAction("Interact");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_animatorManager = m_animatorManagerObject.GetComponent<AnimatorManager>();
    }

    void Update()
    {
        if (m_formManager.CurrentForm == Form.Three)
        {
            var state = LEFT_ACTIVE;

            //  Play Form 3 active animations
            if (m_exchange.IsPressed())
            {
                if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    state = RIGHT_ACTIVE;
                }
            }
            //  Or, play form 3 passive animations
            else
            {
                state = LEFT_PASSIVE;

                if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    state = RIGHT_PASSIVE;
                }
            }

            m_animatorManager.PlayState(state);
        }
    }
}