using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public Vector3 gravity;
    private static float _speed = 100.0f;
    [SerializeField] Vector3 applySpeed = new Vector3(_speed, 0f, 0f);

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
