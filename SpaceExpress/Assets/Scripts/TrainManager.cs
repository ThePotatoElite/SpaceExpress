using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] public float speed = 100.0f;
    private bool _ready = true; // State of the ready button will be 'ON' for now
    private bool _levelDone = false;
    
    void Update()
    {
        if (_ready)
        {
            MoveTrain();
        }
    }

    void MoveTrain()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            speed = 0; // Stop train when hitting flag
            _levelDone = true;
        }
    }
}