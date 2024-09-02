using UnityEngine;

/// <summary>
/// Play Form 0 animations
/// </summary>
public class Form0Animations : MonoBehaviour
{
    const string FORM0 = "Form0";

    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_animatorManagerObject;

    FormManager m_formManager;
    AnimatorManager m_animatorManager;

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