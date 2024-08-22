using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private Transform m_playerPosition;
    [SerializeField] private float m_yOffset;

    private float m_cameraY;

    void Awake()
    {
        m_cameraY = m_playerPosition.transform.position.y + m_yOffset;
    }

    void Update()
    {
        var playerX = m_playerPosition.transform.position.x;

        var cameraPosition = m_playerPosition.transform.position;
        cameraPosition.x = playerX;
        cameraPosition.y = m_cameraY;
        cameraPosition.z = -10.0f;

        m_camera.transform.position = cameraPosition;
    }
}