using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    [SerializeField] private Material levelDoneMaterial;
    private bool _moveDown = false;
    private float _speed = 50f;
    private Collider _collider;
    private AudioManager _audioManager;    
    void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Start()
    {
        _collider = GetComponent<Collider>();
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

    void LevelPassed()
    {
        _audioManager.PlaySFX(_audioManager.levelDone);
        _moveDown = true;
        Destroy(gameObject, 2f); // Match the time before loading the next level
    }
}