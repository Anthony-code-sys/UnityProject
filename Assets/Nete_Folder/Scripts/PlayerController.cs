using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // This variable lets you adjust the butterfly's speed in the Unity Editor
    public float speed = 10f; 
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
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
        }
    }
}