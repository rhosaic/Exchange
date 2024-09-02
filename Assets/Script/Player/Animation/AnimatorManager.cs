using UnityEngine;

/// <summary>
/// Updates the current Player animation
/// </summary>
public class AnimatorManager : MonoBehaviour
{
    public const string FORM0 = "Form0";

    [SerializeField] private Animator m_animator;

    string m_currentState;


    void Awake()
    {
        m_currentState = FORM0;
    }

    /// <summary>
    /// Play animation <c> stateName </c> if it is not already playing
    /// </summary>
    /// <param name="stateName"></param>
    public void PlayState(string stateName)
    {
        if (m_currentState != stateName)
        {
            m_currentState = stateName;

            m_animator.Play(m_currentState);
        }
    }
}
