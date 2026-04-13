using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; 
    
    private Vector3 offset; 

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate runs every frame, but strictly AFTER all physics/movement calculations
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}