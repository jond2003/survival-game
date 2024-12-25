using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputAction pauseInput;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerInput playerInput;

    private bool isPaused = false;


    // Start is called before the first frame update
    void Awake()
    {
        pauseInput = playerInput.actions.FindAction("Pause");
    }


    // Update is called once per frame
    void Update()
    {
        if (pauseInput.WasPerformedThisFrame() && !isPaused)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None; //Hide cursor
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; //Hide cursor
        pauseMenu.SetActive(false);
        isPaused = false;
    }
}
