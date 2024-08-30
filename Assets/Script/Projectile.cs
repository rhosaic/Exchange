using System;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject m_projectileObject;
    [SerializeField] private Transform m_spawnTransform;
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_speed;

    private Vector3 m_projectileDefaultPosition;
    private Vector3 m_spawnPosition;
    private Hitbox m_hitbox;
    private bool m_isMove;
    private bool m_isEnd;

    void Awake()
    {
        m_hitbox = m_projectileObject.GetComponentInChildren<Hitbox>();

        m_projectileDefaultPosition = m_projectileObject.transform.position;
        m_spawnPosition = m_spawnTransform.transform.position;

        m_isEnd = true;
        m_isMove = false;
    }


    void Update()
    {
        if (m_isEnd)
        {
            End();
        }
        else
        {
            if (m_hitbox.IsActive)
            {
                m_isMove = true;
            }
            else
            {
                End();
            }
        }
    }

    void FixedUpdate()
    {
        if (m_isMove)
        {
            Move();
            m_isEnd = false;
        }
    }

    public void Begin()
    {
        m_projectileObject.transform.position = m_spawnPosition;
        m_hitbox.IsActive = true;

        m_isEnd = false;
    }

    public void Move()
    {
        var position = m_projectileObject.transform.position;
        var direction = (m_target.transform.position - position).normalized;

        var directionSlope = direction.y / direction.x;
        var rotationZ = (float)Math.Atan(directionSlope) * Mathf.Rad2Deg;

        if (directionSlope < 0)
        {
            rotationZ += 180;
        }

        var step = Time.fixedDeltaTime * m_speed * direction;

        position += step;

        m_projectileObject.transform.SetPositionAndRotation(
            position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
    }

    public void End()
    {
        m_projectileObject.transform.position = m_projectileDefaultPosition;

        m_isEnd = true;
        m_hitbox.IsActive = false;
    }
}