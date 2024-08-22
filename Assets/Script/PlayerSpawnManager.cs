using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform m_position;
    [SerializeField] private GameObject m_playerObject;

    private bool m_isPlayer;

    void Awake()
    {
        m_isPlayer = false;
    }

    void Update()
    {
        if (!m_isPlayer)
        {
            Instantiate(m_playerObject, m_position);
            m_isPlayer = true;
        }
    }
}