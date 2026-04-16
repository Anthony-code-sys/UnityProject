using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 500f; 
    public TextMeshProUGUI scoreText; 
    public GameObject winTextObject;
    
    private Rigidbody rb;
    private int count; 
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; 
        SetCountText(); 
        winTextObject.SetActive(false); 
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nectar"))
        {
            other.gameObject.SetActive(false);
            count = count + 1; 
            SetCountText(); 
        }
    }

    void SetCountText()
    {
        scoreText.text = "Nectar: " + count.ToString();
        
        if (count >= 5) 
        {
            winTextObject.SetActive(true);
        }
    }
}