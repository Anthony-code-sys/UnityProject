 using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;

    private Vector2 m_moveAmt;
    private Vector2 m_lockAmt;
    private Animator m_animator;
    private Rigidbody m_rigidbody;

    public float WalkSpeed = 5;
    public float RotateSpeed = 5;

    private void OnEnable()
    {
        InputActions.FindActionMap("Butterfly").Enable();
    }

    private void Ondisable()
    {
        InputActions.FindActionMap("Butterfly").Disable();
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");

        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }

    private void Walking()
    {
        m_animator.SetFloat("WalkSpeed", m_moveAmt.y);
        m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * WalkSpeed);
    }

    private void Rotating()
    {
        if (m_moveAmt.y != 0)
        {
            float rotationAmount = m_lockAmt.x * RotateSpeed;
        }
    }

}
