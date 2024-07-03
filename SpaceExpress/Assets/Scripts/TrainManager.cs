using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] public float speed = 100.0f;
    [SerializeField] private Rigidbody _trainRB;
    private int _health = 100;
    private bool _ready = false;
    private bool _levelDone = false;
    public bool _onRail = false;

    public bool Ready { get => _ready; set => _ready = value; }
    public bool OnRail { get => _onRail; set => _onRail = value; }

    void Update()
    {
        if (Ready)
        {
            if (!OnRail)
            MoveTrain();
            else if (OnRail)
            { MoveTrainOnRail(); }
            
        }
    }

    void MoveTrainOnRail()
    {
        _trainRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
    void MoveTrain()
    {
        //_trainRB.constraints = RigidbodyConstraints.None;
        _trainRB.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
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