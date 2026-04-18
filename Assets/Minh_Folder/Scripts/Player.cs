using UnityEngine;
using UnityEngine.InputSystem; 

public class Player : MonoBehaviour
{
    public InputActionAsset InputActions; 

    private InputAction m_moveAction; 
    private InputAction m_lookAction; 
    private InputAction m_jumpAction; 

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    private Animator m_animator; 
    private Rigidbody m_rigidbody; 

    public float WalkSpeed = 5f; 
    public float RotateSpeed = 5f; 
    public float JumpSpeed = 5f; 

    public void Start()
    {
        InputActions = Resources.Load<InputActionAsset>("InputActions"); 
    }


    private void OnEnable()
    {
        InputActions.FindActionMap("Butterfly").Enable(); 
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Butterfly").Disable(); 
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move"); 
        m_lookAction = InputSystem.actions.FindAction("Look"); 
        m_jumpAction = InputSystem.actions.FindAction("Jump"); 

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
        Walking(); 
        Rotating(); 
    }

    private void Walking()
    {
        // m_animator.SetFloat("Speed", m_moveAmt.y); // Set the speed parameter in the Animator based on the movement amount
        m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime); // Move the Rigidbody based on the input and speed
    }

    private void Rotating()
    {
        if (m_moveAmt.y != 0) 
        {
            float rotationAmount = m_lookAmt.x * RotateSpeed * Time.deltaTime; 
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}