using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StrikeManager : MonoBehaviour, IAttack
{
    public int AttackIndex { get => m_attackIndex; set { m_attackIndex = value; } }
    public DamageInfo DamageInfo { get => m_damageInfos[m_attackIndex]; }

    [SerializeField] private List<GameObject> m_attackObjects;
    [SerializeField] private List<BoxCollider2D> m_attackBodies;
    [SerializeField] private List<GameObject> m_damageInfoObjects;
    [SerializeField] private LayerMask m_hitboxLayer;
    [SerializeField] private List<Transform> m_attackDefaultTransforms;
    [SerializeField] private List<Transform> m_attackRightTransforms;
    [SerializeField] private List<Transform> m_attackLeftTransforms;
    [SerializeField] private GameObject m_moveHorizontalObject;

    private List<Vector3> m_attackDefaultPositions;
    private List<DamageInfo> m_damageInfos;
    private bool m_isEnd;
    private int m_attackIndex;
    private int m_attackCount;
    private MoveHorizontal m_moveHorizontal;
    private ContactFilter2D m_filter;

    void Awake()
    {
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();

        m_attackDefaultPositions = new List<Vector3>();
        m_damageInfos = new List<DamageInfo>();

        m_filter.SetLayerMask(m_hitboxLayer);

        foreach (Transform transform in m_attackDefaultTransforms)
        {
            m_attackDefaultPositions.Add(transform.position);
        }

        foreach (GameObject damageInfoObject in m_damageInfoObjects)
        {
            m_damageInfos.Add(damageInfoObject.GetComponent<DamageInfo>());
        }

        m_attackIndex = 0;
        m_attackCount = m_attackObjects.Count;
        m_isEnd = true;
    }

    public void Begin()
    {
        if (m_attackIndex >= 0 && m_attackIndex < m_attackCount)
        {
            var position = m_attackLeftTransforms[m_attackIndex].transform.position;

            if (m_moveHorizontal.Direction == MoveHorizontal.MoveDirection.Right)
            {
                position = m_attackRightTransforms[m_attackIndex].transform.position;
            }

            m_attackObjects[m_attackIndex].transform.position = position;
        }

        m_isEnd = false;
    }

    private void Process()
    {
        var collisions = new Collider2D[1];
        m_attackBodies[m_attackIndex].OverlapCollider(m_filter, collisions);

        foreach (Collider2D collision in collisions)
        {
            if (collision)
            {
                if (collision.TryGetComponent<Hitbox>(out var hitbox))
                {
                    var damageInfo = m_damageInfos[m_attackIndex];

                    if (damageInfo.DamageType == DamageType.Standard)
                    {
                        hitbox.StatusDisplay.Status.Damage(damageInfo.Damage);
                        End();
                    }
                    else if (damageInfo.DamageType == DamageType.Composure)
                    {
                        hitbox.StatusDisplay.Status.Composure.DamageFloored(damageInfo.Damage);
                        End();
                    }
                }
            }
        }
    }

    public void End()
    {
        m_attackObjects[m_attackIndex].transform.position =
            m_attackDefaultPositions[m_attackIndex];

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
            Process();
        }
    }
}