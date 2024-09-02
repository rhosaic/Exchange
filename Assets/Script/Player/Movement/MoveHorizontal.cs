using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MoveDirection { Left, Right };

public class MoveHorizontal : MonoBehaviour
{
    public MoveDirection Direction { get; private set; }

    [SerializeField] InputActionAsset m_inputs;
    [SerializeField] Rigidbody2D m_body;
    [SerializeField] GameObject m_formManagerObject;
    [SerializeField] GameObject m_statusObject;
    [SerializeField] float m_horizontalSpeed;

    InputAction m_moveLeft;
    InputAction m_moveRight;
    FormManager m_formManager;

    void Awake()
    {
        m_inputs.Enable();

        m_moveLeft = m_inputs.FindAction("MoveLeft");
        m_moveRight = m_inputs.FindAction("MoveRight");

        m_formManager = m_formManagerObject.GetComponent<FormManager>();
    }

    void FixedUpdate()
    {
        if (m_formManager.CurrentForm == Form.Zero)
        {
            UpdateMoveHorizontal();
        }
    }
    private void UpdateMoveHorizontal()
    {
        var position = m_body.transform.position;

        if (m_moveLeft.IsPressed())
        {
            position += (Time.fixedDeltaTime * m_horizontalSpeed * Vector3.left);
            Direction = MoveDirection.Left;
        }
        else if (m_moveRight.IsPressed())
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