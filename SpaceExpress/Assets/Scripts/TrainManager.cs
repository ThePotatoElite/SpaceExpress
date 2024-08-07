using UnityEngine;
using System.Collections.Generic;

public class TrainManager : MonoBehaviour
{
    private float _speed;
    [SerializeField] public Vector3 applySpeed;
    [SerializeField] Rigidbody trainRb;
    
    // private Vector3 _beamPosition;
    private Vector3 _initialTrainPosition;
    private Quaternion _initialTrainRotation;
    public float initialSpeed;
    private float _allowedTimeForTravel = 15f;
    private readonly int _health = 100;
    private bool _ready = false;
    private bool _hasStarted = false;
    private bool _onRail = false;
    private bool _isPaused = false;
    public static bool LevelDone = false;
    
    public bool Ready { get => _ready; set => _ready = value; }
    public bool OnRail { get => _onRail; set => _onRail = value; }
    
    private void OnEnable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    void Start()
    {
        Debug.Log(_isPaused);
        _initialTrainPosition = transform.position;
        _initialTrainRotation = transform.rotation;
        initialSpeed = applySpeed.x;
        _speed = initialSpeed;
    }

    void Update()
    {
        Debug.Log(_isPaused);
        if (_isPaused)
        {
            PauseRide();
            return;
        }

        if (Ready)
        {
            if (!_hasStarted)
            {
                StartRide();
                _hasStarted = true;
            }

            if (!OnRail)
            {
                MoveTrain();
            }
            else if (OnRail)
            {
                Debug.Log("On Rail"); // Not Working
                MoveTrainOnRail();
            }

            _allowedTimeForTravel -= Time.deltaTime;
            if (_allowedTimeForTravel <= 0f)
            {
                ResetTrain();
                Debug.Log("Time's up! Resetting train back to its initial position!");
            }
        }
    }
    
    void StartRide()
    {
        _hasStarted = true;
        trainRb.linearVelocity = applySpeed;
        Physics.gravity = new Vector3(-5, 0, 0);
    }

    void PauseRide()
    {
        trainRb.linearVelocity = Vector3.zero;
        _speed = initialSpeed;
        applySpeed = new Vector3(_speed, 0f, 0f);
        Physics.gravity = new Vector3(0, 0, 0);
        _hasStarted = false;
    }
    
    public void SetOnRail(bool onRail/*, Vector3 beamPosition*/)
    {
        _onRail = onRail;
        // _beamPosition = beamPosition;
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
        /*
        trainRb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY;
        trainRb.linearVelocity = Vector3.forward * _speed;
        */
    }
    
    void MoveTrain()
    {
        //trainRb.constraints = RigidbodyConstraints.None;
        trainRb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        // transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        // trainRb.linearVelocity = applySpeed + Vector3.down * 1f; // Apply space-like gravity
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            _speed = 0f; // Stop train when hitting flag
            // applySpeed = new Vector3(_speed, 0f, 0f); // Stop now!
            trainRb.constraints = RigidbodyConstraints.None; // Allow gravity
            trainRb.useGravity = true;
            LevelDone = true;
            Debug.Log("Level completed! Train's HP: " + _health);
            StartCoroutine(LoadNextSceneWithDelay());
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
    
    IEnumerator<WaitForSeconds> LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds before next level
        SceneLoader.LoadNextScene();
        LevelDone = false;
    }
    
    public void ResetTrain()
    {
        transform.position = _initialTrainPosition;
        transform.rotation = _initialTrainRotation;
        _hasStarted = false;
        trainRb.linearVelocity = Vector3.zero;
        _speed = initialSpeed;
        applySpeed = new Vector3(_speed, 0f, 0f);
        Ready = false;
        OnRail = false;
        trainRb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        _allowedTimeForTravel = 15f;
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Pause)
            _isPaused = true;
        else
        {
            _isPaused = false;
        }

    }
}