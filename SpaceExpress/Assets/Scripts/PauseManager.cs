using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;

    void Update()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.Pause && Input.GetMouseButtonDown(0))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.Gameplay || GameStateManager.Instance.CurrentGameState == GameState.Planning)
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
        pauseMenuUI.SetActive(true);
    }

    void UnpauseGame()
    {
        // need to check if the state before was planning or gamplay.
        GameStateManager.Instance.SetState(GameState.Gameplay);
        pauseMenuUI.SetActive(false);
    }
}