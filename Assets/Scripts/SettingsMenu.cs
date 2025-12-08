using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;

    // Make sure these EXACT names match the exposed parameters
    [SerializeField] private string musicParameterName = "musicVol";
    [SerializeField] private string sfxParameterName = "sfxVol";

    [Header("UI")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // Default slider value if nothing is saved yet
    [SerializeField] private float defaultVolume = 0.5f;

    private const string MUSIC_PREF_KEY = "musicVol";
    private const string SFX_PREF_KEY = "sfxVol";

    private void Start()
    {
        LoadVolumeSettings();

        // Optional: hook up listeners here instead of in the Inspector
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(_ => UpdateMusicVolume());

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(_ => UpdateSFXVolume());
    }

    private void LoadVolumeSettings()
    {
        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat(MUSIC_PREF_KEY, defaultVolume);

        if (sfxSlider != null)
            sfxSlider.value = PlayerPrefs.GetFloat(SFX_PREF_KEY, defaultVolume);

        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    public void UpdateMusicVolume()
    {
        if (musicSlider == null || audioMixer == null) return;

        float volume = musicSlider.value;
        SetVolumeOnMixer(musicParameterName, volume);
        PlayerPrefs.SetFloat(MUSIC_PREF_KEY, volume);
        PlayerPrefs.Save();
    }

    public void UpdateSFXVolume()
    {
        if (sfxSlider == null || audioMixer == null) return;

        float volume = sfxSlider.value;
        SetVolumeOnMixer(sfxParameterName, volume);
        PlayerPrefs.SetFloat(SFX_PREF_KEY, volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Converts 0–1 slider value to dB and sets it on the mixer.
    /// </summary>
    private void SetVolumeOnMixer(string parameterName, float sliderValue)
    {
        // Handle mute safely
        if (sliderValue <= 0.0001f)
        {
            // Usually around -80 dB is effectively mute
            audioMixer.SetFloat(parameterName, -80f);
        }
        else
        {
            float dB = Mathf.Log10(sliderValue) * 20f;
            audioMixer.SetFloat(parameterName, dB);
        }
    }
}
