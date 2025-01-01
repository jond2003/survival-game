using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMPro.TextMeshProUGUI volumeText;

    void Start()
    {
        // Check if a volume value exists in PlayerPrefs, if not, set a default value of 1.
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 100f);
        }

        // Load the saved volume value and apply it to the slider.
        Load();
        UpdateVolumeText();
    }

    // Save the new volume value whenever the slider is adjusted.
    public void ChangeVolume()
    {
        AudioListener.volume = Mathf.FloorToInt(volumeSlider.value) / 100f;
        Debug.Log("Changed to " + AudioListener.volume);
        Save();
        UpdateVolumeText();
    }

    // Retrieve the saved volume value and apply it to the slider.
    private void Load()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume");
        volumeSlider.value = savedVolume;

        AudioListener.volume = savedVolume / 100f;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    private void UpdateVolumeText()
    {
        volumeText.text = volumeSlider.value.ToString("0") + "%";
    }
}
