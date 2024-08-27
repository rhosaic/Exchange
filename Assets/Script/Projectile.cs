using System;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class Projectile : MonoBehaviour, IAttack
{
    public DamageInfo DamageInfo { get => m_damageInfo; private set { } }

    [SerializeField] private GameObject m_projectileObject;
    [SerializeField] private GameObject m_damageInfoObject;
    [SerializeField] private Transform m_spawnTransform;
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_speed;
    [SerializeField] private BoxCollider2D m_attackBody;
    [SerializeField] private LayerMask m_hitboxLayer;

    private Vector3 m_projectileDefaultPosition;
    private Vector3 m_spawnPosition;
    private DamageInfo m_damageInfo;
    private bool m_isProcess;
    private bool m_isEnd;
    private ContactFilter2D m_filter;

    void Awake()
    {
        m_damageInfo = m_damageInfoObject.GetComponent<DamageInfo>();

        m_projectileDefaultPosition = m_projectileObject.transform.position;
        m_spawnPosition = m_spawnTransform.transform.position;

        m_filter.SetLayerMask(m_hitboxLayer);

        m_isEnd = true;
    }


    void Update()
    {
        if (m_isEnd)
        {
            End();
        }
        else
        {
            ProcessCollision();
        }
    }

    void FixedUpdate()
    {
        if (m_isProcess)
        {
            Process();
            m_isEnd = false;
        }
    }

    public void Begin()
    {
        m_projectileObject.transform.position = m_spawnPosition;

        m_isProcess = true;
        m_isEnd = false;
    }

    public void Process()
    {
        var position = m_projectileObject.transform.position;
        var direction = (m_target.transform.position - position).normalized;

        var rotationZ = (float)Math.Atan(direction.y / direction.x) * Mathf.Rad2Deg;

        var step = Time.fixedDeltaTime * m_speed * direction;

        position += step;

        m_projectileObject.transform.SetPositionAndRotation(position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
    }

    private void ProcessCollision()
    {
        var collisions = new Collider2D[1];

        m_attackBody.OverlapCollider(m_filter, collisions);

        foreach (Collider2D collision in collisions)
        {
            if (collision)
            {
                if (collision.TryGetComponent<Hitbox>(out var hitbox))
                {
                    if (m_damageInfo.DamageType == DamageType.Standard)
                    {
                        hitbox.StatusDisplay.Status.Damage(m_damageInfo.Damage);
                        End();
                    }
                    else if (m_damageInfo.DamageType == DamageType.Composure)
                    {
                        hitbox.StatusDisplay.Status.Composure.DamageFloored(m_damageInfo.Damage);
                        End();
                    }
                }
            }
        }
    }

    public void End()
    {
        m_projectileObject.transform.position = m_projectileDefaultPosition;

        m_isProcess = false;
        m_isEnd = true;
    }
}