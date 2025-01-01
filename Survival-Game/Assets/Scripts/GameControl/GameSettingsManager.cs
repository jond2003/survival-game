using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private Slider gameTimeSlider;
    [SerializeField] private TMP_Text gameTimeText;

    public static int difficulty = 0;
    public static float gameTimeMinutes = 10f;

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
        gameTimeText.text = ((int)gameTimeSlider.value) + " mins";
        gameTimeMinutes = gameTimeSlider.value;
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
