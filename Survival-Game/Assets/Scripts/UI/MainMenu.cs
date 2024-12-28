using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
