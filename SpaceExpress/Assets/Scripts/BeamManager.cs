using UnityEngine;

public class BeamManager : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material railMaterial;
    [SerializeField] TrainManager trainManager;
    [SerializeField] GameManager gameManager;
    private Vector3 _offset;
    private Vector3 _forceDirection;
    private bool _isDragging = false;
    private bool _hasRail = false;
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
    }
    
    void OnMouseDown() // Pickup Beam
    {
        if (!gameManager.ReadyButton)
        {
            if (gameManager.RailMode)
            {
                _hasRail = true;
                Debug.Log("now you are a rail");
                //   _beamRenderer.material = RailMaterial;
            }
            else if (gameManager.RailMode)
            {
                _hasRail = false;
                //beamRenderer.material = _originalMaterial;
            }
            else if (Input.GetMouseButton(0))
            {
                _isDragging = true;
                _offset = transform.position - GetMouseWorldPosition();
                HighlightBeam(true);
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
        transform.position = GetMouseWorldPosition() + _offset;
    }

    void RotateBeam()
    {
        if (!gameManager.ReadyButton)
        {
            if (Input.GetKey(KeyCode.A)) // Rotate Beam counterclockwise
            {
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D)) // Rotate Beam clockwise
            {
                transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
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
        if (_beamRenderer != null)
        {
            _beamRenderer.material = highlight ? highlightMaterial : _originalMaterial;
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wheels"))
        {
            _hasRail = true;
            trainManager.SetOnRail(_hasRail, transform.position);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wheels"))
        {
            _hasRail = false;
            trainManager.SetOnRail(_hasRail, Vector3.zero);
        }
    }
}