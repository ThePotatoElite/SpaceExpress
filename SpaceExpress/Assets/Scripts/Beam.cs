using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 120f;
    [SerializeField] Material highlightMaterial;
    [SerializeField] GameManager gameManager;
    private Vector3 _offset;
    // private Vector3 _forceDirection;
    private bool _isDragging = false;
    private bool _isCollidingWithBeam = false;
    private bool _isCollidingWithPlayer = false;
    private bool _isPlacing = false; // Should I remove this entirely and switch all to _isDragging?
    private Camera _mainCamera;
    private Material _originalMaterial;
    private Renderer _beamRenderer;
    private AudioManager _audioManager;

    void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            DragBeam();
            RotateBeam();
            RemoveBeam();
        }
        else
        {
            HighlightBeam(_isPlacing);
            // HighlightBeam(_isDragging);
        }
    }
    
    void OnMouseDown() // Pickup Beam
    {
        if (!gameManager.DriveMode)
        {
            if (Input.GetMouseButton(0)) // if (!_isDragging)? - Something different to try?
            {
                _isDragging = true;
                _audioManager.PlaySFX(_audioManager.pickup);
                _offset = transform.position - GetMouseWorldPosition();
            }
        }
    }

    void OnMouseUp() // Place Beam
    {
        if (Input.GetMouseButtonUp(0)) // if (_isDragging)? - Something different to try?
        {
            _isDragging = false;
            _isPlacing = false;
            _audioManager.PlaySFX(_audioManager.place);
            HighlightBeam(false);
        }
    }

    void DragBeam()
    {
        if (!_isCollidingWithBeam && !_isCollidingWithPlayer)
        {
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
        if (!gameManager.DriveMode)
        {
            // Handle keyboard input
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Rotate Beam counterclockwise
            {
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Rotate Beam clockwise
            {
                transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime, Space.Self);
            }

            // Handle mobile input
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                // Determine which touch is the second finger
                Touch secondTouch = touch2;

                // Calculate the delta position of the second touch
                Vector2 secondTouchDelta = secondTouch.deltaPosition;

                // Determine rotation direction based on the y-direction of the second touch movement
                if (secondTouchDelta.y > 0)
                {
                    // Second finger moved up - rotate beam clockwise
                    transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime, Space.Self);
                }
                else if (secondTouchDelta.y < 0)
                {
                    // Second finger moved down - rotate beam counterclockwise
                    transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
                }
            }
        }
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