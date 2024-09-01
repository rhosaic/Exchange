using UnityEngine;
using UnityEngine.InputSystem;

public class Form3Animations : MonoBehaviour
{
    private const string LEFT_ACTIVE = "Form3LeftActive";
    private const string LEFT_PASSIVE = "Form3LeftPassive";
    private const string RIGHT_ACTIVE = "Form3RightActive";
    private const string RIGHT_PASSIVE = "Form3RightPassive";

    [SerializeField] private InputActionAsset m_inputs;
    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_moveHorizontalObject;
    [SerializeField] private GameObject m_animatorManagerObject;

    private InputAction m_exchange;
    private FormManager m_formManager;
    private MoveHorizontal m_moveHorizontal;
    private AnimatorManager m_animatorManager;

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

            if (m_exchange.IsPressed())
            {
                if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    state = RIGHT_ACTIVE;
                }
            }
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