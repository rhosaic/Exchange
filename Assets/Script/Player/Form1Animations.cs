using UnityEngine;

public class Form1Animations : MonoBehaviour
{
    private const string LEFT_ACTIVE = "Form1LeftActive";
    private const string LEFT_PASSIVE = "Form1LeftPassive";
    private const string RIGHT_ACTIVE = "Form1RightActive";
    private const string RIGHT_PASSIVE = "Form1RightPassive";

    const float ACTIVE_TIME = 0.3f;

    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_moveHorizontalObject;
    [SerializeField] private GameObject m_animatorManagerObject;

    private FormManager m_formManger;
    private MoveHorizontal m_moveHorizontal;
    private AnimatorManager m_animatorManager;

    void Awake()
    {
        m_formManger = m_formManagerObject.GetComponent<FormManager>();
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_animatorManager = m_animatorManagerObject.GetComponent<AnimatorManager>();
    }

    void Update()
    {
        if (m_formManger.CurrentForm == FormManager.Form.One)
        {
            var state = LEFT_ACTIVE;

            if (m_moveHorizontal.Direction == MoveHorizontal.MoveDirection.Left)
            {
                if (m_formManger.Form1Time < ACTIVE_TIME)
                {
                    state = LEFT_ACTIVE;
                }
                else
                {
                    state = LEFT_PASSIVE;
                }
            }

            if (m_moveHorizontal.Direction == MoveHorizontal.MoveDirection.Right)
            {
                if (m_formManger.Form1Time < ACTIVE_TIME)
                {
                    state = RIGHT_ACTIVE;
                }
                else
                {
                    state = RIGHT_PASSIVE;
                }
            }

            m_animatorManager.PlayState(state);
        }
    }
}
