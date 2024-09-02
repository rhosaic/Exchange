using UnityEngine;

/// <summary>
/// Play Form 1 animations
/// </summary>
public class Form1Animations : MonoBehaviour
{
    const string LEFT_ACTIVE = "Form1LeftActive";
    const string LEFT_PASSIVE = "Form1LeftPassive";
    const string RIGHT_ACTIVE = "Form1RightActive";
    const string RIGHT_PASSIVE = "Form1RightPassive";

    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_animatorManagerObject;
    [SerializeField] GameObject m_blockObject;

    FormManager m_formManger;
    MoveHorizontal m_moveHorizontal;
    AnimatorManager m_animatorManager;
    Block m_block;

    void Awake()
    {
        m_formManger = m_formManagerObject.GetComponent<FormManager>();
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_animatorManager = m_animatorManagerObject.GetComponent<AnimatorManager>();
        m_block = m_blockObject.GetComponent<Block>();
    }

    void Update()
    {
        if (m_formManger.CurrentForm == Form.One)
        {
            var state = LEFT_PASSIVE;

            //  Play Form 1 active states
            if (m_block.IsBlockActive)
            {
                state = LEFT_ACTIVE;

                if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    state = RIGHT_ACTIVE;
                }
            }
            //  Or, play Form 1 passive states
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
