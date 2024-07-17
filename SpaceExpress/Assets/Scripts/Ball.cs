using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody ballRb;
    public Vector3 gravity;
    public Vector3 ballSpeed;
    
    void Start()
    {
        Physics.gravity = gravity;
    }
    
    void Update()
    {
        ballSpeed = ballRb.linearVelocity;
        Physics.gravity = gravity;
    }
}