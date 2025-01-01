using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputAction pauseInput;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject HUDUI;
    [SerializeField] private PlayerInput playerInput;

    public static bool isPaused = false;

    float beforePauseVolume; //Used to mute during pause 

    // Start is called before the first frame update
    void Awake()
    {
        pauseInput = playerInput.actions.FindAction("Pause");
    }


    // Update is called once per frame
    void Update()
    {
        if (pauseInput.WasPerformedThisFrame())
        {
            if (!isPaused) PauseGame();
            else ResumeGame();
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None; //Show cursor
        pauseMenu.SetActive(true);
        HUDUI.SetActive(false);
        isPaused = true;
        beforePauseVolume = Mathf.FloorToInt(AudioListener.volume);
        AudioListener.volume = 0f;
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; //Hide cursor
        pauseMenu.SetActive(false);
        HUDUI.SetActive(true);
        isPaused = false;

        if (AudioListener.volume == 0f && beforePauseVolume != 0f) //They may have changed it during pause
        {
            AudioListener.volume = beforePauseVolume;
        }
        
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(0);
    }
}
