using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] TrainManager _trainManager;
    [SerializeField] GameObject _beam;
    private bool beamMode = false;

    public bool BeamMode { get => beamMode; set => beamMode = value; }

    private void Update()
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
            Debug.Log("Should Work");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit: " + hit.point);
             GameObject newBeam = Instantiate(_beam);
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

    public void StartDrive()
    {
        _trainManager.Ready = true;
    }
}

