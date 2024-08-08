using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    private bool _moveDown = false;
    private float _speed = 50f;
    private Collider _collider;
    // [SerializeField] private Material levelDoneMaterial;
    
    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (_moveDown)
        {
            Debug.Log("Moving Flag Down..."); // NOT WORKING
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelPassed();
        }
    }

    void LevelPassed() // Should change to what actually happens when you hit the flag
    {
        _collider.enabled = false; // Disable collision test 1
        GetComponent<Collider>().enabled = false; // Disable collision test 2
        _moveDown = true;
        // levelDoneMaterial.color = Color.green;
        Destroy(gameObject, 2f); // Match the time before loading the next level
        Debug.Log("Flag should be moving down with a disabled collider..."); // NOT WORKING
        // Insert Win Logic Later
    }
}