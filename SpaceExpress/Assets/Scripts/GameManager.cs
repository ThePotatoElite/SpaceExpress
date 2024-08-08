using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
// using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TrainManager trainManager;
    [SerializeField] TextMeshProUGUI tutorial;
    [SerializeField] TextMeshProUGUI wellDone;
    [SerializeField] TextMeshProUGUI timeIsUpMessage;
    // [SerializeField] GameObject beamPrefab;
    // [SerializeField] GameObject mainCamera;
    // [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    // private GameObject _currentBeam;
    // private bool _isPlacingBeam = false;
    private bool _driveMode = false;
    public Vector3 gravity;
    
    public bool DriveMode { get => _driveMode; set => _driveMode = value; }

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
    }
    
    public void Rewind()
    {
        TrainManager.LevelDone = false;
        _driveMode = false;
        wellDone.gameObject.SetActive(false);
        trainManager.ResetTrain();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Celebration()
    {
        _driveMode = false;
        wellDone.gameObject.SetActive(true);
    }

    public IEnumerator<WaitForSeconds> ShowTimeIsUpMessage()
    {
        timeIsUpMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2); // Show for 2 seconds
        timeIsUpMessage.gameObject.SetActive(false);
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
            beam.transform.Rotate(Vector3.right, 120f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            beam.transform.Rotate(Vector3.right, -120f * Time.deltaTime);
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