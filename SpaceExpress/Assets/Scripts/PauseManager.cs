using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    // private bool _isPaused = false;

    void Update()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.Pause && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
            {
                TogglePauseMenu();
            }
        }
    }

    private void TogglePauseMenu()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.Gameplay)
        {
            PauseGame();
        }
        else if (GameStateManager.Instance.CurrentGameState == GameState.Pause)
        {
            UnpauseGame();
        }
    }

    void PauseGame()
    {
        GameStateManager.Instance.SetState(GameState.Pause);
        //Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    void UnpauseGame()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
        //Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}