using UnityEngine;
using TMPro;

public class MountainPlayerController : MonoBehaviour
{
    [Header("Flight Settings")]
    public float moveSpeed = 10f;
    public float verticalSpeed = 8f;
    private Rigidbody rb;

    [Header("Garden Growth Mechanics")]
    public GameObject flowerPrefab; 
    public int winGoal = 100;
    private int flowersPlanted = 0;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
        
        flowersPlanted = 0;
        UpdateUI();
        winTextObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && flowersPlanted < winGoal)
        {
            PlantFlower();
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float moveY = 0f;
        if (Input.GetKey(KeyCode.E)) moveY = 1f;
        if (Input.GetKey(KeyCode.Q)) moveY = -1f;

        Vector3 moveDirection = new Vector3(moveX, moveY, moveZ).normalized;
        
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void PlantFlower()
{
    Vector3 spawnPos = transform.position + Vector3.down * 0.5f;
    GameObject newFlower = Instantiate(flowerPrefab, spawnPos, Quaternion.identity);

    newFlower.SetActive(true);

    flowersPlanted++;
    UpdateUI();

    if (flowersPlanted >= winGoal)
    {
        winTextObject.SetActive(true);
        scoreText.text="";
    }
}

    void UpdateUI()
    {
        scoreText.text = "Flowers Planted: " + flowersPlanted + " / " + winGoal;
    }
}