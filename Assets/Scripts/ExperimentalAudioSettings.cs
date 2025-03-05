using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ExperimentalAudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicSet"))
        {
            LoadVolume();
        }
        else
        { 
            SetMusicVolume();
            SetSFXVolume();
        } 
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicSet", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfxVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxSet", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicSet");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxSet");

        SetMusicVolume();
        SetSFXVolume();
    }
}
