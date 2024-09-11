using UnityEngine;
using UnityEngine.UI;

public class Beam : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 120f;
    [SerializeField] Material highlightMaterial;
    [SerializeField] GameManager gameManager;
    [SerializeField] Slider rotationSlider; 

    private Vector3 _offset;
    private bool _isDragging = false;
    private bool _isCollidingWithBeam = false;
    private bool _isCollidingWithPlayer = false;
    private bool _isPlacing = false;
    private Camera _mainCamera;
    private Material _originalMaterial;
    private Renderer _beamRenderer;
    private AudioManager _audioManager;

    void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rotationSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _beamRenderer = GetComponent<Renderer>();
        if (_beamRenderer != null)
        {
            _originalMaterial = _beamRenderer.material;
        }
    }

    void Update()
    {
        if (_isDragging)
        {
            if (CanRotate())
            {
                RotateBeam();
            }
            else
            {
                DragBeam();
                RemoveBeam();
            }
        }
        else
        {
            HighlightBeam(_isPlacing);
        }
    }

    void OnMouseDown() 
    {
        if (!gameManager.DriveMode)
        {
            if (Input.GetMouseButton(0))
            {
                _isDragging = true;
                _audioManager.PlaySFX(_audioManager.pickup);
                _offset = transform.position - GetMouseWorldPosition();
            }
        }
    }

    void OnMouseUp() 
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _isPlacing = false;
            _audioManager.PlaySFX(_audioManager.place);
            HighlightBeam(false);

           
            if (rotationSlider != null)
            {
                rotationSlider.value = 0f;
            }
        }
    }

    void DragBeam()
    {
        if (!_isCollidingWithBeam && !_isCollidingWithPlayer)
        {
            if(Input.touchCount < 2)
            transform.position = GetMouseWorldPosition() + _offset;
            HighlightBeam(true);
        }
        else if (_isCollidingWithBeam || _isCollidingWithPlayer)
        {
            transform.position = GetMouseWorldPosition() + _offset;
            HighlightBeam(false);
        }
    }

    void RotateBeam()
    {
        if (!gameManager.DriveMode && rotationSlider != null)
        {
            float sliderValue = rotationSlider.value;

            if (Mathf.Approximately(sliderValue, 1f))
            {
                transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime, Space.Self);
            }
            else if (Mathf.Approximately(sliderValue, -1f))
            {
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    bool CanRotate()
    {
        if (rotationSlider == null)
        {
            return false;
        }

        float sliderValue = rotationSlider.value;
        return Mathf.Approximately(sliderValue, 1f) || Mathf.Approximately(sliderValue, -1f);
    }

    void RemoveBeam()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
            _audioManager.PlaySFX(_audioManager.destroy);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
        return _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    void HighlightBeam(bool highlight)
    {
        if (_beamRenderer)
        {
            _beamRenderer.material = highlight ? highlightMaterial : _originalMaterial;
        }
    }

    public void SetPlacing(bool isPlacing)
    {
        _isPlacing = isPlacing;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beam"))
        {
            _isCollidingWithBeam = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _isCollidingWithPlayer = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beam"))
        {
            _isCollidingWithBeam = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _isCollidingWithPlayer = false;
        }
    }
}