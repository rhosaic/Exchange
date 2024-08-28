using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireProjectile : MonoBehaviour
{
    public bool IsActive { get => m_isActive; set { m_isActive = value; } }

    [SerializeField] private float m_minimumFireDelay;
    [SerializeField] private float m_maximumFireDelay;
    [SerializeField] private GameObject m_projectileObject;
    [SerializeField] private GameObject m_statusDisplayObject;
    [SerializeField] private Light2D m_light;
    [SerializeField] private float m_lightIntensityMaximum;
    [SerializeField] private float m_warmUpTimeMaximum;

    private Projectile m_projectile;
    private StatusDisplay m_status;
    private float m_fireDelayDifference;
    private float m_fireTime;
    private bool m_isActive;
    private float m_warmUpTime;

    void Awake()
    {
        m_projectile = m_projectileObject.GetComponent<Projectile>();
        m_status = m_statusDisplayObject.GetComponent<StatusDisplay>();

        m_fireDelayDifference = m_maximumFireDelay - m_minimumFireDelay;
        m_fireTime = 0.0f;
        m_isActive = false;
        m_warmUpTime = 0.0f;
    }

    void Update()
    {
        if (m_isActive)
        {
            if (m_warmUpTime < m_warmUpTimeMaximum)
            {
                m_warmUpTime += Time.deltaTime;
                m_light.intensity =
                    m_lightIntensityMaximum
                    * (m_warmUpTime / m_warmUpTimeMaximum);
            }
            else
            {
                Fire();
            }
        }
        else
        {
            m_warmUpTime = 0.0f;
            m_light.intensity = 0.0f;
            m_status.Status.Composure.HealCapped(
                m_status.Status.Composure.Maximum);
        }
    }

    private void Fire()
    {
        var fireDelay =
            m_maximumFireDelay
            - (m_status.Status.Composure.PercentRemaining()
            * m_fireDelayDifference);

        m_fireTime += Time.deltaTime;

        if (m_fireTime > fireDelay)
        {
            m_projectile.Begin();

            m_fireTime = 0.0f;
        }
    }
}