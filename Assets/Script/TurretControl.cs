using UnityEngine;

public class TurretControl : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject m_fireProjectileObject;

    private FireProjectile m_fireProjectile;

    void Awake()
    {
        m_fireProjectile =
            m_fireProjectileObject.GetComponent<FireProjectile>();
    }

    public void Interact()
    {
        if (m_fireProjectile.IsActive)
        {
            m_fireProjectile.IsActive = false;
        }
        else
        {
            m_fireProjectile.IsActive = true;
        }
    }
}