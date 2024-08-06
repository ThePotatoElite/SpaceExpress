using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // [SerializeField] GameObject settingsMenu;
    
    // public AudioManager audioManager;
    
    public void Pause()
    {
        GameStateManager.Instance.SetState(GameState.Pause);
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Placeholder
    }
    
    public void Resume()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
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