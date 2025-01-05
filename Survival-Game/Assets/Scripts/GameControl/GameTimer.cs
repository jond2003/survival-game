using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numOfDaysText;
    [SerializeField] private DayNightCycle dayNightCycle; // Reference to DayNightCycle

    private void Update()
    {
        UpdateDayDisplay();
        CheckWinCondition();
    }

    private void UpdateDayDisplay()
    {
        float maxDays = GameSettingsManager.numofDays;
        numOfDaysText.text = $"Day {dayNightCycle.dayNumber} / {maxDays}";
    }

    private void CheckWinCondition()
    {
        if (dayNightCycle.dayNumber >= GameSettingsManager.numofDays)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync("WinScene");
        }
    }
}
