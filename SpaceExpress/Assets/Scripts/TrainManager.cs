using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] public float speed = 100.0f;
    private int _health = 100;
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
            Debug.Log("Level completed! Train's HP: " + _health);
        }
        /*
        else if (other.CompareTag("Obstacle"))
        {
            _health -= 10; // Subtract HP when hitting an obstacle or when in long airtime (fall damage)?
            speed = (speed * massOfTrain)/(massOfTrain + massOfObstacle); // Subtract speed by Inelastic collision physics?
            Debug.Log("Train's HP: " + _health + "\n" + "Train's Speed: " + speed);
        }
        */
    }
}