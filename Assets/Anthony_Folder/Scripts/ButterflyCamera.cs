using UnityEngine;

public class ButterflyCamera : MonoBehaviour
{
    [Header("Tracking Settings")]
    public Transform target;        
    public Vector3 offset = new Vector3(0, 5, -10); 
    
    [Header("Movement Smoothness")]
    public float smoothTime = 0.3f; 
    private Vector3 currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        transform.LookAt(target);
    }
}