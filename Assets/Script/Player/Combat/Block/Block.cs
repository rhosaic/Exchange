using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Set and activate the BlockBox
/// </summary>
public class Block : MonoBehaviour
{
    //  Length of time BlockBox is active
    public static float ActiveTime = 0.15f;

    public bool IsBlockActive { get => m_blockTime < ActiveTime; set { } }

    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_moveHorizontalObject;
    [SerializeField] GameObject m_blockboxObject;
    [SerializeField] Transform m_leftBlockTransform;
    [SerializeField] Transform m_rightBlockTransform;

    FormManager m_formManager;
    MoveHorizontal m_moveHorizontal;
    BlockBox m_blockbox;
    Vector3 m_defaultPosition;
    float m_blockTime;

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

            // Identify the block position
            var blockPosition = m_leftBlockTransform.transform.position;

            if (m_moveHorizontal.Direction == MoveDirection.Right)
            {
                blockPosition = m_rightBlockTransform.transform.position;
            }

            //  Set BlockBox
            if (IsBlockActive)
            {
                m_blockboxObject.transform.position = blockPosition;

                m_blockbox.IsActive = true;
            }
            //  Or, remove BlockBox
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