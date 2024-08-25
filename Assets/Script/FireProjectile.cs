using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] private float m_fireDelay;
    [SerializeField] private GameObject m_projectileObject;

    private Projectile m_projectile;
    private float m_fireTime;

    void Awake()
    {
        m_projectile = m_projectileObject.GetComponent<Projectile>();
    }

    void Update()
    {
        m_fireTime += Time.deltaTime;

        if (m_fireTime >= m_fireDelay)
        {
            m_fireTime = 0.0f;

            m_projectile.Despawn();
            m_projectile.Spawn();
        }
    }
}