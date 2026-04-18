using UnityEngine;
using UnityEngine.InputSystem; // Required for Input System

public class Player : MonoBehaviour
{
    public InputActionAsset InputActions; // Reference to the Input Action Asset

    private InputAction m_moveAction; // Reference to the move action
    private InputAction m_lookAction; // Reference to the look action
    private InputAction m_jumpAction; // Reference to the jump action

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    private Animator m_animator; // Reference to the Animator component
    private Rigidbody m_rigidbody; // Reference to the Rigidbody component

    public float WalkSpeed = 5f; // Speed of the player when walking
    public float RotateSpeed = 5f; // Speed of the player when rotating
    public float JumpSpeed = 5f; // Speed of the player when jumping

    public void Start()
    {
        //InputActions = Resources.Load<InputActionAsset>("InputActions"); // Load the Input Action Asset from Resources folder
    }


    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable(); // Enable the Player action map
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable(); // Disable the Player action map
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move"); // Find the Move action in the Input Action Asset
        m_lookAction = InputSystem.actions.FindAction("Look"); // Find the Look action in the Input Action Asset
        m_jumpAction = InputSystem.actions.FindAction("Jump"); // Find the Jump action in the Input Action Asset

        m_rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the player
        //m_animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>(); // Read the value of the Move action
        m_lookAmt = m_lookAction.ReadValue<Vector2>(); // Read the value of the Look action

        if (m_jumpAction.WasPressedThisFrame()) // Check if the Jump action was triggered
        {
            Jump(); // Call the Jump method
        }
    }

    public void Jump()
    {
        // Perform the jump action
        m_rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse); // Add an upward force to the Rigidbody
        // m_animator.SetTrigger("Jump"); // Trigger the jump animation in the Animator
    }

    private void FixedUpdate()
    {
        Walking(); // Call the Walking method in FixedUpdate for physics calculations
        Rotating(); // Call the Rotating method in FixedUpdate for physics calculations
    }

    private void Walking()
    {
        // m_animator.SetFloat("Speed", m_moveAmt.y); // Set the speed parameter in the Animator based on the movement amount
        m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime); // Move the Rigidbody based on the input and speed
    }

    private void Rotating()
    {
        if (m_moveAmt.y != 0) // Check if the player is moving on Y
        {
            float rotationAmount = m_lookAmt.x * RotateSpeed * Time.deltaTime; // Calculate the rotation amount based on the input and speed
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation); // Rotate the Rigidbody based on the input and speed
        }
    }
}