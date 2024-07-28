using TMPro;
using UnityEngine;
// using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TrainManager trainManager;
    [SerializeField] TextMeshProUGUI tutorial;
    [SerializeField] TextMeshProUGUI wellDone;
    // [SerializeField] GameObject beamPrefab;
    
    public Vector3 gravity;
    private bool _driveMode = false;
    // private bool _isPlacingBeam = false;
    // private GameObject _currentBeam;
    public bool DriveMode { get => _driveMode; set => _driveMode = value; }

    // [SerializeField] GameObject mainCamera;
    // [SerializeField] CinemachineVirtualCamera cinemachineCamera;

    void Update()
    {
        Physics.gravity = gravity;
        if (TrainManager.LevelDone)
        {
            Celebration();
        }
        /*
        if (_isPlacingBeam && _currentBeam != null)
        {
            FollowMouse(_currentBeam);
            RotateBeam(_currentBeam);
        }

        if (_isPlacingBeam && Input.GetMouseButtonDown(0))
        {
            PlaceBeam();
        }
        */
    }

    public void StartDriveMode()
    {
        tutorial.gameObject.SetActive(false);
        trainManager.Ready = true;
        trainManager.applySpeed = new Vector3(trainManager.initialSpeed, 0f, 0f);
        _driveMode = true;
        // SwitchToCinemachineCamera();
    }

    public void RestartScene()
    {
        TrainManager.LevelDone = false;
        _driveMode = false;
        wellDone.gameObject.SetActive(false);
        trainManager.ResetTrain();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    /*
    public void EnterRailMode()
    {
        _railMode = !_railMode;
    }
    */

    void Celebration()
    {
        _driveMode = false;
        wellDone.gameObject.SetActive(true);
    }
    /*
    public void SpawnBeam()
    {
        _currentBeam = Instantiate(beamPrefab);
        _isPlacingBeam = true;
    }
    
    void FollowMouse(GameObject beam)
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(beam.transform.position).z;
        beam.transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
    
    void RotateBeam(GameObject beam)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            beam.transform.Rotate(Vector3.right, 100f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            beam.transform.Rotate(Vector3.right, -100f * Time.deltaTime);
        }
    }
    
    void PlaceBeam()
    {
        _isPlacingBeam = false;
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