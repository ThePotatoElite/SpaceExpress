using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] public float speed = 100.0f;
    [SerializeField] Rigidbody trainRb;
    private Vector3 _beamPosition;
    private int _health = 100;
    private bool _ready = false;
    public static bool _levelDone = false;
    public bool _onRail = false;
    
    public bool Ready { get => _ready; set => _ready = value; }
    public bool OnRail { get => _onRail; set => _onRail = value; }

    void Update()
    {
        if (Ready)
        {
            if (!OnRail)
            { MoveTrain(); }
            else if (OnRail)
            { MoveTrainOnRail(); }
        }
    }
    
    public void SetOnRail(bool onRail, Vector3 beamPosition)
    {
        _onRail = onRail;
        _beamPosition = beamPosition;
        if (onRail)
        {
            trainRb.AddForce(Vector3.down * 10f, ForceMode.Acceleration); // Apply Beam Force
        }
        else
        {
            trainRb.AddForce(Vector3.zero, ForceMode.Acceleration); // Remove Beam Force
        }
    }

    void MoveTrainOnRail()
    {
        trainRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
    
    void MoveTrain()
    {
        //trainRb.constraints = RigidbodyConstraints.None;
        trainRb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
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