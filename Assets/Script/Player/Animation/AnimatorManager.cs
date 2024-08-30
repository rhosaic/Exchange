using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    private string m_currentState;

    public const string FORM0 = "Form0";

    void Awake()
    {
        m_currentState = FORM0;
    }

    public void PlayState(string stateName)
    {
        if (m_currentState != stateName)
        {
            m_currentState = stateName;

            m_animator.Play(m_currentState);
        }
    }
}
