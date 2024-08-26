using UnityEngine;

public enum DamageType { Standard, Composure, Status };

public class Attack : MonoBehaviour
{
    public bool IsDamage { get => m_isDamage; set { m_isDamage = value; } }
    public float Damage { get => m_damage; private set { } }
    public DamageType DamageType { get => m_damageType; private set { } }

    [SerializeField] private float m_damage;
    [SerializeField] private DamageType m_damageType;

    private bool m_isDamage;

    void Awake()
    {
        m_isDamage = true;
    }
}