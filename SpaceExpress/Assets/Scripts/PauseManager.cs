using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private bool _isPaused = false;

    void Update()
    {
        if (_isPaused && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
            {
                UnpauseGame();
            }
        }
    }

    public void TogglePauseMenu()
    {
        _isPaused = !_isPaused;
        pauseMenuUI.SetActive(_isPaused);

        if (_isPaused)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        _isPaused = true;
        pauseMenuUI.SetActive(true);
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    private bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}