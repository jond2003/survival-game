using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] Slider gameVolumeSlider;
    [SerializeField] TMPro.TextMeshProUGUI gameVolumeText;

    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] TMPro.TextMeshProUGUI musicVolumeText;

    void Start()
    {
        // Load the saved volume value and apply it to the slider.
        LoadGameVolume();
        UpdateGameVolumeText();

        LoadMusicVolume();
        UpdateMusicVolumeText();
    }

    // Save the new volume value whenever the slider is adjusted.
    public void ChangeGameVolume()
    {
        SoundManager.SaveGameVolume(gameVolumeSlider.value);
        UpdateGameVolumeText();
    }

    public void ChangeMusicVolume()
    {
        SoundManager.SaveMusicVolume(musicVolumeSlider.value);
        UpdateMusicVolumeText();
    }

    // Retrieve the saved volume value and apply it to the slider.
    private void LoadGameVolume()
    {
        gameVolumeSlider.value = SoundManager.GetGameVolume();
    }

    private void LoadMusicVolume()
    {
        musicVolumeSlider.value = SoundManager.GetMusicVolume();
    }

    private void UpdateGameVolumeText()
    {
        gameVolumeText.text = (((gameVolumeSlider.value - 0.0001) / (1 - 0.0001)) * 100).ToString("0") + "%";
    }

    private void UpdateMusicVolumeText()
    {
        musicVolumeText.text = (((musicVolumeSlider.value - 0.0001) / (1 - 0.0001)) * 100).ToString("0") + "%";
    }
}
