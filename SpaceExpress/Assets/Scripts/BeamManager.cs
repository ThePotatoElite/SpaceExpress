using UnityEngine;

public class BeamManager : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] Color highlightColor = Color.yellow;
    private bool _isDragging = false;
    private Vector3 _offset;
    private Camera _mainCamera;
    private Color _originalColor;
    private Renderer _beamRenderer;
    
    void Start()
    {
        _mainCamera = Camera.main;
        _beamRenderer = GetComponent<Renderer>();
        if (_beamRenderer != null)
        {
            _originalColor = _beamRenderer.material.color;
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
    
    private void OnMouseDown() // Pickup Beam
    {
        if (Input.GetMouseButton(0))
        {
            _isDragging = true;
            _offset = transform.position - GetMouseWorldPosition();
            HighlightBeam(true);
        }
    }

    private void OnMouseUp() // Leave Beam
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            HighlightBeam(false);
        }
    }

    private void DragBeam()
    {
        transform.position = GetMouseWorldPosition() + _offset;
    }

    private void RotateBeam()
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

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
        return _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
    
    private void HighlightBeam(bool highlight)
    {
        if (_beamRenderer != null)
        {
            _beamRenderer.material.color = highlight ? highlightColor : _originalColor;
        }
    }
}