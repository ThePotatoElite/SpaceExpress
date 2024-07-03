using System;
using UnityEngine;

public class BeamManager : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] Material highlightMaterial;
    [SerializeField] TrainManager trainManager;
    private ConstantForce _constantForce;
    private bool _isDragging = false;
    private bool _hasRail = false;
    private Vector3 _offset;
    private Vector3 _forceDirection;
    private Camera _mainCamera;
    private Material _originalMaterial;
    private Renderer _beamRenderer;
    
    void Start()
    {
        _mainCamera = Camera.main;
        _beamRenderer = GetComponent<Renderer>();
        _constantForce = GetComponent<ConstantForce>();
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
            _beamRenderer.material = highlight ? highlightMaterial : _originalMaterial;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wheels"))
        {
            // trainManager.OnRail = true;
            _hasRail = true;
            StickBeam();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wheels"))
        {
            // trainManager.OnRail = false;
            _hasRail = false;
            StickBeam();
        }
    }
    
    private void StickBeam()
    {
        if (_hasRail)
        {
            _forceDirection = new Vector3(0, -100, 0);
            _constantForce.force = _forceDirection;
        }
        else
        {
            _forceDirection = new Vector3(0, 0, 0);
            _constantForce.force = _forceDirection;
        }
    }
}