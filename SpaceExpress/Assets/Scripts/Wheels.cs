using UnityEngine;

public class TrainWheelScript : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Move the train along the x-axis
        rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, rb.linearVelocity.z);
    }
}