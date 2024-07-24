using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // [SerializeField] GameObject settingsMenu;
    
    // public AudioManager audioManager;
    
    public void Pause()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.Pause
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Placeholder
    }
    
    public void Resume()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.Pause
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Placeholder
    }
    
    /*
    public void Settings()
    {
        settingsMenu.SetActive(true);
    }

    public void Return()
    {
        settingsMenu.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    */

    public void ExitGame()
    {
        Application.Quit();
    }
}