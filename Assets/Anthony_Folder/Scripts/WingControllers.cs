using UnityEngine;

public class WingFlapper : MonoBehaviour
{
    [Header("Connect the SHOULDER objects here:")]
    public Transform leftWing;
    public Transform rightWing;

    [Header("Flap Speed")]
    public float flapSpeed = 20f; 

    [Header("The Symphony (Axes)")]
    [Tooltip("How high and low the wings go (The Door Hinge)")]
    public float upDownAmplitude = 60f; 
    
    [Tooltip("How much the wing twists forward and back (The Pitch)")]
    public float twistAmplitude = 15f; 

    private Vector3 leftStartRot;
    private Vector3 rightStartRot;

    void Start()
    {
        // Memorize the starting flat rotation
        if (leftWing != null) leftStartRot = leftWing.localEulerAngles;
        if (rightWing != null) rightStartRot = rightWing.localEulerAngles;
    }

    void Update()
    {
        float upDownAngle = Mathf.Sin(Time.time * flapSpeed) * upDownAmplitude;
        
        float twistAngle = Mathf.Cos(Time.time * flapSpeed) * twistAmplitude;
        
        if (leftWing != null)
        {
            leftWing.localEulerAngles = new Vector3(
                leftStartRot.x, 
                leftStartRot.y + upDownAngle, 
                leftStartRot.z + twistAngle
            );
        }
        
        if (rightWing != null)
        {
            rightWing.localEulerAngles = new Vector3(
                rightStartRot.x, 
                rightStartRot.y - upDownAngle, 
                rightStartRot.z - twistAngle 
            );
        }
    }
}