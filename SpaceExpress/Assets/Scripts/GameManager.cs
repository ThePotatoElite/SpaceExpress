using UnityEngine;
// using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TrainManager trainManager;
    // [SerializeField] GameObject mainCamera;
    // [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    
    // [SerializeField] GameObject beam;
    // private bool _beamMode = false;

    // public bool BeamMode { get => _beamMode; set => _beamMode = value; }
    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointnClick();
        }
    }

    public void PointnClick()
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
        trainManager.Ready = true;
        // SwitchToCinemachineCamera();
    }
    /*
    void SwitchToCinemachineCamera()
    {
        mainCamera.SetActive(false);
        cinemachineCamera.gameObject.SetActive(true);
    }
    */
}