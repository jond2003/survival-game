using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    [SerializeField] Slider sensitivitySlider;

    void Start()
    {
        // Check if a sensitivity value exists in PlayerPrefs, if not, set a default value of 1.
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetFloat("Sensitivity", 1f);
        }

        // Load the saved sensitivity value and apply it to the slider.
        Load();
    }

    // Save the new sensitivity value whenever the slider is adjusted.
    public void ChangeSensitivity()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
    }

    // Retrieve the saved sensitivity value and apply it to the slider.
    private void Load()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }
}
