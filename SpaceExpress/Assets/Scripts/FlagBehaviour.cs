using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    [SerializeField] private Material levelDoneMaterial;
    private bool _moveDown = false;
    private float _speed = 50f;
    private Collider _collider;
    private AudioManager _audioManager;
    private Renderer _flagRenderer;
    
    void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Start()
    {
        _collider = GetComponent<Collider>();
        _flagRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_moveDown)
        {
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
        _audioManager.PlaySFX(_audioManager.levelDone);
        _flagRenderer.material = levelDoneMaterial; // NOT WORKING
        _moveDown = true;
        Destroy(gameObject, 2f); // Match the time before loading the next level
        // Insert Win Logic Later
    }
}