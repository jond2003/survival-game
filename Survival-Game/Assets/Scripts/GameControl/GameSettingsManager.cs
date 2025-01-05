using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private Slider numOfDaysSlider;
    [SerializeField] private Slider gameDayLengthSlider;

    [SerializeField] private TMP_Text numOfDaysText;
    [SerializeField] private TMP_Text gameDaylengthText;

    public static int difficulty = 0;
    public static float numofDays = 5f;
    public static float gameTimeMinutes = 5f;

    public static GameSettingsManager Instance { get; private set; }

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void SetDifficulty(int index)
    {
        difficulty = index;
    }

    public void SetGameTime()
    {
        numOfDaysText.text = ((int)numOfDaysSlider.value) + " days";
        numofDays = numOfDaysSlider.value;
    }

    public void SetGameLengthOfDay()
    {
        gameDaylengthText.text = ((int)gameDayLengthSlider.value) + " min";
        gameTimeMinutes = gameDayLengthSlider.value;
    }

    // Takes 3 scriptable objects representing the easy, hard and impossible versions
    // Returns the correct scriptable (data) to use for the assigned difficulty
    public static ScriptableObject GetDifficultyData(ScriptableObject easy, ScriptableObject hard, ScriptableObject impossible)
    {
        ScriptableObject scriptableObject;
        switch (difficulty)
        {
            case 0:
                scriptableObject = easy;
                break;
            case 1:
                scriptableObject = hard;
                break;
            case 2:
                scriptableObject = impossible;
                break;
            default:
                scriptableObject = easy;
                break;
        }
        return scriptableObject;
    }
}
