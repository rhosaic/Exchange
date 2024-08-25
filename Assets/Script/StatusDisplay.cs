using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    public Status Status { get => m_status; private set { } }
    [SerializeField] private float m_composureMaximum;
    [SerializeField] private float m_healthMaximum;
    [SerializeField] private Slider m_composureSlider;
    [SerializeField] private Slider m_healthSlider;

    private Status m_status;

    void Awake()
    {
        m_status
            = new Status(m_composureMaximum, m_healthMaximum);

        m_composureSlider.maxValue = m_status.Composure.Maximum;
        m_healthSlider.maxValue = m_status.Health.Maximum;
    }

    void Update()
    {
        m_composureSlider.value = m_status.Composure.Current;
        m_healthSlider.value = m_status.Health.Current;
    }
}