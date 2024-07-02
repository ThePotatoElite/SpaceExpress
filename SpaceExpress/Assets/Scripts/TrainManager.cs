using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] public float speed = 100.0f;
    private bool _ready = false;
    private bool _levelDone = false;

    public bool Ready { get => _ready; set => _ready = value; }

    void Update()
    {
        if (Ready)
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