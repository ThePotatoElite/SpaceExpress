using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
// using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TrainManager trainManager;
    [SerializeField] TextMeshProUGUI tutorial;
    [SerializeField] TextMeshProUGUI wellDone;
    [SerializeField] GameObject beamPrefab;
    [SerializeField] GameObject railPrefab;
    // [SerializeField] GameObject mainCamera;
    // [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    private GameObject _currentBeam;
    private bool _isPlacingBeam = false;
    private AudioManager _audioManager;
    private bool _driveMode = false;
    public Vector3 gravity;
    
    public bool DriveMode { get => _driveMode; set => _driveMode = value; }

    void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Update()
    {
        Physics.gravity = gravity;
        
        if (TrainManager.LevelDone)
        {
            Celebration();
        }
        
        if (_isPlacingBeam && _currentBeam != null)
        {
            FollowMouse(_currentBeam);
            RotateBeam(_currentBeam);
        }

        if (_isPlacingBeam && Input.GetMouseButtonDown(0))
        {
            PlaceBeam();
        }
        
    }

    public void StartDriveMode()
    {
        _audioManager.PlaySFX(_audioManager.start);
        tutorial.gameObject.SetActive(false);
        trainManager.Ready = true;
        trainManager.applySpeed = new Vector3(trainManager.initialSpeed, 0f, 0f);
        _driveMode = true;
        Time.timeScale = 2f; // Increase the gameplay speed to 2
        // SwitchToCinemachineCamera();
    }

    public void RestartScene()
    {
        _audioManager.PlaySFX(_audioManager.restart);
        TrainManager.LevelDone = false;
        _driveMode = false;
        wellDone.gameObject.SetActive(false);
        trainManager.ResetTrain();
        Time.timeScale = 1f; // Return the gameplay speed to 1
    }
    
    public void Rewind()
    {
        TrainManager.LevelDone = false;
        _driveMode = false;
        wellDone.gameObject.SetActive(false);
        trainManager.ResetTrain();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Return the gameplay speed to 1
    }

    void Celebration()
    {
        _driveMode = false;
        wellDone.gameObject.SetActive(true);
        Time.timeScale = 1f; // Return the gameplay speed to 1
    }
    
    public void SpawnBeam()
    {
        if (!trainManager.Ready)
        {
            _currentBeam = Instantiate(beamPrefab);
            _isPlacingBeam = true;
            _audioManager.PlaySFX(_audioManager.pickup);
            Beam beamScript = _currentBeam.GetComponent<Beam>();
            if (beamScript != null)
            {
                beamScript.SetPlacing(true);
            }
        }
    }
    
    public void SpawnRail()
    {
        if (!trainManager.Ready)
        {
            _currentBeam = Instantiate(railPrefab);
            _isPlacingBeam = true;
            _audioManager.PlaySFX(_audioManager.pickup);
            Beam beamScript = _currentBeam.GetComponent<Beam>();
            if (beamScript != null)
            {
                beamScript.SetPlacing(true);
            }
        }
    }

    void FollowMouse(GameObject beam)
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(beam.transform.position).z;
        beam.transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    void RotateBeam(GameObject beam)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Rotate Beam counterclockwise
        {
            beam.transform.Rotate(Vector3.right, 120f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Rotate Beam clockwise
        {
            beam.transform.Rotate(Vector3.right, -120f * Time.deltaTime);
        }
    }

    void PlaceBeam()
    {
        _isPlacingBeam = false;
        _audioManager.PlaySFX(_audioManager.place);
        _currentBeam = null;
    }
    /*
    void SwitchToCinemachineCamera()
    {
        mainCamera.SetActive(false);
        cinemachineCamera.gameObject.SetActive(true);
    }
    */
}