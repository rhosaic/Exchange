using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveHorizontal : MonoBehaviour
{
    public enum MoveDirection { Left, Right };
    [SerializeField] private InputActionAsset m_inputs;
    [SerializeField] private Rigidbody2D m_body;
    [SerializeField] private GameObject m_formManagerObject;
    [SerializeField] private GameObject m_statusObject;
    [SerializeField] private float m_horizontalSpeed;

    private InputAction m_moveLeft;
    private InputAction m_moveRight;
    private FormManager m_formManager;
    public MoveDirection Direction { get; private set; }

    void Awake()
    {
        m_inputs.Enable();

        m_moveLeft = m_inputs.FindAction("MoveLeft");
        m_moveRight = m_inputs.FindAction("MoveRight");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
    }

    void FixedUpdate()
    {
        if (m_formManager.CurrentForm == FormManager.Form.Zero)
        {
            UpdateMoveHorizontal();
        }
    }
    private void UpdateMoveHorizontal()
    {
        var moveLeftValue = m_moveLeft.ReadValue<float>();
        var moveRightValue = m_moveRight.ReadValue<float>();

        var position = m_body.transform.position;

        if (moveLeftValue > float.Epsilon)
        {
            position += (Time.fixedDeltaTime * m_horizontalSpeed * Vector3.left);
            Direction = MoveDirection.Left;
        }
        else if (moveRightValue > float.Epsilon)
        {
            position += (Time.fixedDeltaTime * m_horizontalSpeed * Vector3.right);
            Direction = MoveDirection.Right;
        }

        m_body.transform.position = position;

        var rotation = m_body.transform.rotation;
        rotation.z = 0;

        m_body.transform.rotation = rotation;
    }

}