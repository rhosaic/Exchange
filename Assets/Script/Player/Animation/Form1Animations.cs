using UnityEngine;

public class Form1Animations : MonoBehaviour
{
    private const string LEFT_ACTIVE = "Form1LeftActive";
    private const string LEFT_PASSIVE = "Form1LeftPassive";
    private const string RIGHT_ACTIVE = "Form1RightActive";
    private const string RIGHT_PASSIVE = "Form1RightPassive";

    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_moveHorizontalObject;
    [SerializeField] private GameObject m_animatorManagerObject;
    [SerializeField] private GameObject m_blockObject;

    private FormManager m_formManger;
    private MoveHorizontal m_moveHorizontal;
    private AnimatorManager m_animatorManager;
    private Block m_block;

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

            if (m_block.IsBlockActive)
            {
                state = LEFT_ACTIVE;

                if (m_moveHorizontal.Direction == MoveDirection.Right)
                {
                    state = RIGHT_ACTIVE;
                }
            }
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
