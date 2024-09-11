using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class TrainManager : MonoBehaviour
{
    [SerializeField] Slider fuelBar;
    [SerializeField] public Vector3 applySpeed;
    [SerializeField] Rigidbody trainRb;
    [SerializeField] TextMeshProUGUI timeIsUpMessage;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject readyButton;
    // private Vector3 _beamPosition;
    private AudioManager _audioManager;
    private Vector3 _initialTrainPosition;
    private Quaternion _initialTrainRotation;
    private int _health = 100;
    private bool _ready = false;
    private bool _hasStarted = false;
    private bool _onRail = false;
    private bool _isPaused = false;
    private float _speed;
    public float initialSpeed;
    public float allowedTimeForTravel = 15f;
    public static bool LevelDone = false;
    
    private bool OnRail { get => _onRail; set => _onRail = value; }
    public bool Ready { get => _ready; set => _ready = value; }
    
    void OnEnable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDisable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    
    void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Start()
    {
        _initialTrainPosition = transform.position;
        _initialTrainRotation = transform.rotation;
        initialSpeed = applySpeed.x;
        _speed = initialSpeed;
    }

    void Update()
    {
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
                Debug.Log("On Rail"); // NOT WORKING
            }

            allowedTimeForTravel -= Time.deltaTime;
            AdjustSlider();
            if (allowedTimeForTravel <= 0f)
            {
                _audioManager.PlaySFX(_audioManager.levelTimeOut);
                ResetTrain();
                restartButton.SetActive(false);
                readyButton.SetActive(true);
                StartCoroutine(ShowTimeIsUpMessage());
            }
        }
    }

    public void AdjustSlider()
    {
        fuelBar.value = allowedTimeForTravel;
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
    
    public void SetOnRail(bool onRail)
    {
        _onRail = onRail;
        if (onRail)
        {
            trainRb.AddForce(Vector3.down * 10f, ForceMode.Acceleration); // Apply Beam Force
        }
        else
        {
            trainRb.AddForce(Vector3.zero, ForceMode.Acceleration); // Remove Beam Force
        }
    }
    
    void MoveTrain()
    {
        trainRb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            // _speed = 0f; // Stop train when hitting flag
            // applySpeed = new Vector3(_speed, 0f, 0f); // Stop now!
            trainRb.constraints = RigidbodyConstraints.None; // Allow gravity
            trainRb.useGravity = true;
            LevelDone = true;
            Debug.Log("Level completed! Train's HP: " + _health);
            StartCoroutine(LoadNextSceneWithDelay());
        }
        else if (other.CompareTag("Platform") || other.CompareTag("Wagon"))
        {
            _health -= 25;
            Debug.Log("Train's hit! Its HP is: " + _health);

            if (_health <= 0)
            {
                Debug.Log("Oh no! Train destroyed!");
                _audioManager.PlaySFX(_audioManager.explode);
                ResetTrain();
                StartCoroutine(ShowTimeIsUpMessage());
            }
            else
            {
                _audioManager.PlaySFX(_audioManager.getHit);
            }
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
        trainRb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        trainRb.linearVelocity = Vector3.zero;
        _speed = initialSpeed;
        applySpeed = new Vector3(_speed, 0f, 0f);
        Ready = false;
        OnRail = false;
        _health = 100;
        allowedTimeForTravel = 15f;
    }
    
    void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Pause)
            _isPaused = true;
        else
        {
            _isPaused = false;
        }
    }

    private IEnumerator ShowTimeIsUpMessage()
    {
        timeIsUpMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); 
        timeIsUpMessage.gameObject.SetActive(false);
    }
}