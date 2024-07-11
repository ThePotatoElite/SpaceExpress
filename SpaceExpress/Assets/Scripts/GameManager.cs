using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
// using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TrainManager trainManager;
    [SerializeField] TextMeshProUGUI tutorial;
    [SerializeField] TextMeshProUGUI wellDone;

    private bool _railMode;
    private bool _readyButton = false;

    public bool RailMode { get => _railMode; set => _railMode = value; }
    public bool ReadyButton { get => _readyButton; set => _readyButton = value; }
    
    // [SerializeField] GameObject mainCamera;
    // [SerializeField] CinemachineVirtualCamera cinemachineCamera;

    // [SerializeField] GameObject beam;
    // private bool _beamMode = false;

    // public bool BeamMode { get => _beamMode; set => _beamMode = value; }

    void Update()
    {
        if (TrainManager.LevelDone)
        {
            Celebration();
        }
    }
    /*
    public void pointAndClick()
    {
        if (BeamMode)
        {
            Debug.Log("Should Work...");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.point);
                GameObject newBeam = Instantiate(beam);
                newBeam.transform.position = hit.point;
            }
        }
    }

    public void EnableBeam()
    {
        if (BeamMode)
        {
            BeamMode = false;
        }
        else { BeamMode = true; }
    }
    */
    public void StartDrive()
    {
        tutorial.gameObject.SetActive(false);
        trainManager.Ready = true;
        _readyButton = true;

        // SwitchToCinemachineCamera();
    }

    public void RestartScene()
    {
        TrainManager.LevelDone = false;
        _readyButton = false;
        wellDone.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void EnterRailMode()
    {
        if (_railMode) { _railMode = false; }
        else _railMode = true;
    }
    void Celebration()
    {
        _readyButton = false;
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