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
    }
    
    public void Resume()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
        pauseMenu.SetActive(false);
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