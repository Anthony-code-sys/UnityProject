using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    public InputActionAsset InputActions;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject winButton;

    private InputAction m_moveAction; 
    private InputAction m_lookAction;
 

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    private Animator m_animator;
    private Rigidbody m_rigidbody;
    private int count;

    public float WalkSpeed = 5f; 
    public float RotateSpeed = 5f;
    public float JumpSpeed = 5f;

    public void Start()
    {
        InputActions = Resources.Load<InputActionAsset>("InputActions");
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        winButton.SetActive(false);
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
      
        m_rigidbody = GetComponent<Rigidbody>();
    }   

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>(); 
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

       
        }



        private void FixedUpdate()
    {
        Walking(); 
        Rotating(); 
    }

    private void Walking()
    { 
        m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime); 
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

    void SetCountText()
    {
        countText.text = "Flower Collected: " + count.ToString();
        if(count >= 6)
        {
            winTextObject.SetActive(true);
            winButton.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
   
    }

    

}