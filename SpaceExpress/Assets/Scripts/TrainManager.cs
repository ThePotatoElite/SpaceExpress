using UnityEngine;
using System.Collections.Generic;

public class TrainManager : MonoBehaviour
{
    private static float _speed = 100.0f;
    [SerializeField] Vector3 applySpeed = new Vector3(_speed,0f,0f);
    [SerializeField] Rigidbody trainRb;
    private Vector3 _beamPosition;
    private int _health = 100;
    private bool _ready = false;
    public static bool LevelDone = false;
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
        trainRb.linearVelocity = Vector3.forward * _speed;
    }
    
    void MoveTrain()
    {
        //trainRb.constraints = RigidbodyConstraints.None;
        trainRb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        // transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        trainRb.linearVelocity = applySpeed + Vector3.down * 1f; // Apply space-like gravity
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            _speed = 0f; // Stop train when hitting flag
            applySpeed = new Vector3(_speed, 0f, 0f); // Stop now!
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
    
    private IEnumerator<WaitForSeconds> LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds before next level
        SceneLoader.LoadNextScene();
        LevelDone = false;
    }
}