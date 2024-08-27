using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] private float m_minimumFireDelay;
    [SerializeField] private float m_maximumFireDelay;
    [SerializeField] private GameObject m_projectileObject;
    [SerializeField] private GameObject m_statusDisplayObject;

    private Projectile m_projectile;
    private StatusDisplay m_status;
    private float m_fireDelayDifference;
    private float m_fireTime;

    void Awake()
    {
        m_projectile = m_projectileObject.GetComponent<Projectile>();
        m_status = m_statusDisplayObject.GetComponent<StatusDisplay>();

        m_fireDelayDifference = m_maximumFireDelay - m_minimumFireDelay;
    }

    void Update()
    {
        var fireDelay = m_maximumFireDelay - (m_status.Status.Composure.PercentRemaining() * m_fireDelayDifference);
        m_fireTime += Time.deltaTime;

        if (m_fireTime > fireDelay)
        {
            m_projectile.Begin();

            m_fireTime = 0.0f;
        }
    }
}