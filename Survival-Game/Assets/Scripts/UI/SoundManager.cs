using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    [SerializeField] Slider gameVolumeSlider;
    [SerializeField] TMPro.TextMeshProUGUI gameVolumeText;


    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] TMPro.TextMeshProUGUI musicVolumeText;

    void Start()
    {
        // Check if a game volume value exists in PlayerPrefs, if not, set a default value of 1.
        if (!PlayerPrefs.HasKey("GameVolume"))
        {
            PlayerPrefs.SetFloat("GameVolume", 100f);
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 100f);
        }



        // Load the saved volume value and apply it to the slider.
        LoadGameVolume();
        UpdateGameVolumeText();

        LoadMusicVolume();
        UpdateMusicVolumeText();
    }

    // Save the new volume value whenever the slider is adjusted.
    public void ChangeGameVolume()
    {
        audioMixer.SetFloat("GameVolume", Mathf.Log10(gameVolumeSlider.value) * 20f);
        SaveGameVolume();
        UpdateGameVolumeText();
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolumeSlider.value) * 20f);
        SaveMusicVolume();
        UpdateMusicVolumeText();
    }

    // Retrieve the saved volume value and apply it to the slider.
    private void LoadGameVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("GameVolume");
        gameVolumeSlider.value = savedVolume;

        audioMixer.SetFloat("GameVolume", Mathf.Log10(gameVolumeSlider.value) * 20f);
    }

    private void LoadMusicVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicVolumeSlider.value = savedVolume;

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolumeSlider.value) * 20f);
    }

    private void SaveGameVolume()
    {
        PlayerPrefs.SetFloat("GameVolume", gameVolumeSlider.value);
    }
    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    private void UpdateGameVolumeText()
    {
        gameVolumeText.text = (((gameVolumeSlider.value - 0.0001) / (1 - 0.0001)) * 100) .ToString("0") + "%";
    }

    private void UpdateMusicVolumeText()
    {
        musicVolumeText.text = (((musicVolumeSlider.value - 0.0001) / (1 - 0.0001)) * 100).ToString("0") + "%";
    }
}
