using UnityEngine;

public class WingFlapper : MonoBehaviour
{
    [Header("Wing Connections")]
    public Transform leftWing;
    public Transform rightWing;

    [Header("Flap Settings")]
    public float flapSpeed = 20f; 
    public float flapAngle = 45f; 

    void Update()
    {  
        float angle = Mathf.Sin(Time.time * flapSpeed) * flapAngle;
        
        if (leftWing != null)
        {
            leftWing.localRotation = Quaternion.Euler(90f, 0f, angle);
        }
        
        if (rightWing != null)
        {
            rightWing.localRotation = Quaternion.Euler(90f, 0f, -angle);
        }
    }
}