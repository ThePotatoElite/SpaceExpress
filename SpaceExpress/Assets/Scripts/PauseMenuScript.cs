using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button muteButton;
    // [SerializeField] Slider musicVolumeSlider;
    // [SerializeField] Slider sfxVolumeSlider;
    // [SerializeField] GameObject settingsMenu;
    
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
    public void MuteButton()
    {
        AudioManager.Instance.ToggleMute();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    /*
    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
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

}