using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
// using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TrainManager trainManager;
    [SerializeField] TextMeshProUGUI tutorial;
    [SerializeField] TextMeshProUGUI wellDone;

    private bool _setRailMode;
    private bool _driveMode = false;

    public bool SetRailMode { get => _setRailMode; set => _setRailMode = value; }
    public bool DriveMode { get => _driveMode; set => _driveMode = value; }


    // [SerializeField] GameObject mainCamera;
    // [SerializeField] CinemachineVirtualCamera cinemachineCamera;

    void Update()
    {
        if (TrainManager.LevelDone)
        {
            Celebration();
        }
    }

    public void StartDriveMode()
    {
        tutorial.gameObject.SetActive(false);
        trainManager.Ready = true;
        _driveMode = true;

        // SwitchToCinemachineCamera();
    }

    public void RestartScene()
    {
        TrainManager.LevelDone = false;
        _driveMode = false;
        wellDone.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void EnterRailMode()
    {
        if (_setRailMode) { _setRailMode = false; }
        else _setRailMode = true;
    }
    void Celebration()
    {
        _driveMode = false;
        wellDone.gameObject.SetActive(true);
    }
    /*
    void SwitchToCinemachineCamera()
    {
        mainCamera.SetActive(false);
        cinemachineCamera.gameObject.SetActive(true);
    }
    */
}