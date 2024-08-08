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
    // private bool _isPlacing = false;
    private Camera _mainCamera;
    private Material _originalMaterial;
    private Renderer _beamRenderer;

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
        }
        /*
        if (_isPlacing)
        {
            HighlightBeam(true);
        }
        else
        {
            HighlightBeam(false);
        }
        */
    }
    
    void OnMouseDown() // Pickup Beam
    {
        if (!gameManager.DriveMode)
        {
            if (Input.GetMouseButton(0))
            {
                _isDragging = true;
                _offset = transform.position - GetMouseWorldPosition();
            }
        }
    }

    void OnMouseUp() // Leave Beam
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            HighlightBeam(false);
        }
    }

    void DragBeam()
    {
        if (!_isCollidingWithBeam)
        {
            transform.position = GetMouseWorldPosition() + _offset;
            HighlightBeam(true);
        }
        else if (_isCollidingWithBeam)
        {
            transform.position = GetMouseWorldPosition() + _offset;
            HighlightBeam(false);
        }
    }

    void RotateBeam()
    {
        if (!gameManager.DriveMode)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Rotate Beam counterclockwise
            {
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Rotate Beam clockwise
            {
                transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
            }
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
    /*
    public void SetPlacing(bool isPlacing)
    {
        _isPlacing = isPlacing;
    }
    */
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beam"))
        {
            _isCollidingWithBeam = true;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beam"))
        {
            _isCollidingWithBeam = false;
        }
    }
}