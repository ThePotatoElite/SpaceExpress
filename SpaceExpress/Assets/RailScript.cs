using UnityEngine;
using UnityEngine.EventSystems;

public class RailScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] MeshRenderer _railMeshRenderer;
    private bool _isActive = false;
    public void ChangeRail()
    {
        if (_isActive) { _railMeshRenderer.enabled = false; _isActive = false; }
        else if (!_isActive) { _railMeshRenderer.enabled = true; _isActive = true; }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeRail();
    }
}
