using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGameSettings()
    {
        SceneManager.LoadSceneAsync("PregameMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadSceneAsync("SettingsScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

}
