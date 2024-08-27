using UnityEngine;

public enum DamageType { Standard, Composure, Status };

public class DamageInfo : MonoBehaviour
{
    public float Damage { get => m_damage; private set { } }
    public DamageType DamageType { get => m_damageType; private set { } }
    public IAttack Attack { get => m_attack; private set { } }

    [SerializeField] private float m_damage;
    [SerializeField] private DamageType m_damageType;
    [SerializeField] private GameObject m_attackObject;

    private IAttack m_attack;

    void Awake()
    {
        m_attack = m_attackObject.GetComponent<IAttack>();
    }
}