using UnityEngine;

public class Block : MonoBehaviour
{
    public static float ActiveTime = 0.15f;

    public bool IsBlockActive { get => m_blockTime < ActiveTime; private set { } }

    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_blockboxObject;
    [SerializeField] Transform m_leftBlockTransform;
    [SerializeField] Transform m_rightBlockTransform;

    private FormManager m_formManager;
    private MoveHorizontal m_moveHorizontal;
    private BlockBox m_blockbox;
    private Vector3 m_defaultPosition;
    private float m_blockTime;

    void Awake()
    {
        m_formManager = m_formManagerObject.GetComponent<FormManager>();
        m_moveHorizontal = m_moveHorizontalObject.GetComponent<MoveHorizontal>();
        m_blockbox = m_blockboxObject.GetComponent<BlockBox>();

        m_defaultPosition = m_blockbox.Collider.transform.position;
    }

    void Update()
    {
        if (m_formManager.CurrentForm == Form.One)
        {
            m_blockTime += Time.deltaTime;

            var blockPosition = m_leftBlockTransform.transform.position;

            if (m_moveHorizontal.Direction == MoveDirection.Right)
            {
                blockPosition = m_rightBlockTransform.transform.position;
            }

            if (IsBlockActive)
            {
                m_blockboxObject.transform.position = blockPosition;

                m_blockbox.IsActive = true;
            }
            else
            {
                m_blockboxObject.transform.position = m_defaultPosition;

                m_blockbox.IsActive = false;
            }
        }
        else
        {
            m_blockbox.transform.position = m_defaultPosition;

            m_blockTime = 0.0f;
        }
    }
}