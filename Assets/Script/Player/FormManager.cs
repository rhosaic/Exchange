using UnityEngine;
using UnityEngine.InputSystem;

public class FormManager : MonoBehaviour
{
    public enum Form { Zero, One, Two, Three };
    public Form CurrentForm { get; private set; }
    [SerializeField] private InputActionAsset m_inputs;

    public float Form1Time { get => m_form1Time; private set { } }
    public bool IsForm3Ascending { get => m_isForm3Ascending; private set { } }

    private float m_form1Time;
    private InputAction m_form1;
    private InputAction m_form2;
    private InputAction m_form3;
    private int m_form3PressCount;
    private bool m_isForm3Ascending;

    void Awake()
    {
        m_inputs.Enable();

        m_form1 = m_inputs.FindAction("EnterForm1");
        m_form2 = m_inputs.FindAction("EnterForm2");
        m_form3 = m_inputs.FindAction("EnterForm3");

        CurrentForm = Form.Zero;

        m_form3PressCount = 0;
        m_form1Time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateForm1();
        UpdateForm2();
        UpdateForm3();
    }

    private void UpdateForm1()
    {
        UpdateForm1Time();

        if (m_form1.IsPressed())
        {
            CurrentForm = Form.One;
        }

        if ((CurrentForm == Form.One) && m_form1.WasReleasedThisFrame())
        {
            CurrentForm = Form.Zero;
        }
    }

    private void UpdateForm2()
    {
        if (m_form2.IsPressed())
        {
            CurrentForm = Form.Two;
        }

        if ((CurrentForm == Form.Two) && m_form2.WasReleasedThisFrame())
        {
            CurrentForm = Form.Zero;
        }
    }

    private void UpdateForm1Time()
    {
        if (CurrentForm == Form.One)
        {
            m_form1Time += Time.deltaTime;
        }
        else
        {
            m_form1Time = 0.0f;
        }
    }

    private void UpdateForm3()
    {
        if (m_form3.WasPressedThisFrame())
        {
            if (CurrentForm != Form.Three)
            {
                CurrentForm = Form.Three;
                m_isForm3Ascending = true;
                m_form3PressCount += 1;
            }
        }

        if (m_isForm3Ascending)
        {
            if (m_form3.WasReleasedThisFrame())
            {
                m_isForm3Ascending = false;
            }
        }

        if ((CurrentForm == Form.Three) && !m_isForm3Ascending)
        {
            if (m_form3.WasPressedThisFrame())
            {
                CurrentForm = Form.Zero;
            }
        }
    }
}

