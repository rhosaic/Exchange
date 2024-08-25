using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject m_projectileObject;
    [SerializeField] private Transform m_spawnPosition;
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_speed;

    private GameObject m_projectile;
    private Attack m_attack;

    void Awake()
    {
        m_projectile = null;
    }

    void FixedUpdate()
    {
        if (m_projectile)
        {
            if (m_attack.IsDamage)
            {
                var position = m_projectile.transform.position;
                var direction = (m_target.transform.position - position).normalized;

                var rotationZ = (float)Math.Atan(direction.y / direction.x) * Mathf.Rad2Deg;

                var step = Time.fixedDeltaTime * m_speed * direction.normalized;

                position += step;

                m_projectile.transform.position = position;

                m_projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            }
            else
            {
                Despawn();
            }
        }
    }

    public GameObject Spawn()
    {
        var projectile = Instantiate(m_projectileObject, m_spawnPosition);

        m_projectile = projectile;
        m_attack = projectile.GetComponentInChildren<Attack>();
        m_attack.IsDamage = true;

        return projectile;
    }

    private void Despawn()
    {
        Destroy(m_projectile);
    }
}