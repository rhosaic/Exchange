using UnityEngine;
using UnityEngine.InputSystem;

public enum Form { Zero, One, Two, Three };

public class FormManager : MonoBehaviour
{
    public Form CurrentForm { get; private set; }

    [SerializeField] InputActionAsset m_inputs;

    InputAction m_form1;
    InputAction m_form2;
    InputAction m_form3;
    bool m_isForm3Ascending;

    void Awake()
    {
        m_inputs.Enable();

        m_form1 = m_inputs.FindAction("EnterForm1");
        m_form2 = m_inputs.FindAction("EnterForm2");
        m_form3 = m_inputs.FindAction("EnterForm3");

        CurrentForm = Form.Zero;
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

    private void UpdateForm3()
    {
        if (m_form3.WasPressedThisFrame())
        {
            if (CurrentForm != Form.Three)
            {
                CurrentForm = Form.Three;
                m_isForm3Ascending = true;
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

