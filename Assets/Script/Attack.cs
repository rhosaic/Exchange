using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool IsDamage { get => m_isDamage; set { m_isDamage = value; } }
    public float Damage { get => m_damage; private set { } }

    [SerializeField] private float m_damage;

    private bool m_isDamage;

    void Awake()
    {
        m_isDamage = true;
    }
}