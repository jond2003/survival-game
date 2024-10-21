using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private float startTime = 60.00f;
    private float currentTime;


    void Awake()
    {
        currentTime = startTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int minutes = Mathf.FloorToInt(currentTime / 60);
        string timeFormatted = string.Format("{0:00}:{1:00}", minutes, seconds);

       

        if (currentTime <= 0f) //Won game
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(2);
        }

        countDownText.text = timeFormatted;
    }
}
