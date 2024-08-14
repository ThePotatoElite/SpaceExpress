using UnityEngine;
using UnityEngine.EventSystems;

public class RailScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] MeshRenderer railMeshRenderer;
    private bool _isActive = false;
    
    public void ChangeRail()
    {
        if (_isActive) { railMeshRenderer.enabled = false; _isActive = false; }
        else if (!_isActive) { railMeshRenderer.enabled = true; _isActive = true; }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeRail();
    }
}