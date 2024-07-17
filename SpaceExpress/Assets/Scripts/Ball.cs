using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public Vector3 gravity;

    public Vector3 Speed;
    void Start()
    {
        Physics.gravity = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        Speed = rb.linearVelocity;
        Physics.gravity = gravity;
    }
}
