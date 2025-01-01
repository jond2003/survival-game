using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countDownText;
    private float currentTime;


    void Awake()
    {
        currentTime = GameSettingsManager.gameTimeMinutes * 60;
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
