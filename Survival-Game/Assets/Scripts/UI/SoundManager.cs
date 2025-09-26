using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private static string gameVolumeKey = "GameVolume";
    private static string musicVolumeKey = "MusicVolume";

    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Check if a game volume value exists in PlayerPrefs, if not, set a default value of 100
        if (!PlayerPrefs.HasKey(gameVolumeKey))
        {
            PlayerPrefs.SetFloat(gameVolumeKey, 100f);
        }

        if (!PlayerPrefs.HasKey(musicVolumeKey))
        {
            PlayerPrefs.SetFloat(musicVolumeKey, 100f);
        }

        LoadGameVolume(GetGameVolume());
        LoadMusicVolume(GetMusicVolume());
    }

    // Set the saved volumes
    private void LoadGameVolume(float volume) { audioMixer.SetFloat(gameVolumeKey, Mathf.Log10(volume) * 20f); }
    private void LoadMusicVolume(float volume) { audioMixer.SetFloat(musicVolumeKey, Mathf.Log10(volume) * 20f); }

    // Get the saved volume levels
    public static float GetGameVolume() { return PlayerPrefs.GetFloat(gameVolumeKey); }
    public static float GetMusicVolume() { return PlayerPrefs.GetFloat(musicVolumeKey); }

    // Update the volume and save to storage
    public static void SaveGameVolume(float newVolume)
    {
        Instance.LoadGameVolume(newVolume);
        PlayerPrefs.SetFloat(gameVolumeKey, newVolume);
    }
    public static void SaveMusicVolume(float newVolume)
    {
        Instance.LoadMusicVolume(newVolume);
        PlayerPrefs.SetFloat(musicVolumeKey, newVolume);
    }

    public static void SelfDestruct()
    {
        Destroy(Instance.gameObject);
    }
}
