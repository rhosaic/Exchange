using UnityEngine;

public class Form0Animations : MonoBehaviour
{
    private const string FORM0 = "Form0";

    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_animatorManagerObject;

    private FormManager m_formManager;
    private AnimatorManager m_animatorManager;

    void Awake()
    {
        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_animatorManager = m_animatorManagerObject.GetComponent<AnimatorManager>();
    }

    void Update()
    {
        if (m_formManager.CurrentForm == Form.Zero)
        {
            m_animatorManager.PlayState(FORM0);
        }
    }
}