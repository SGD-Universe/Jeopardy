using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // script unfinished, still needs to assign values to sliders, and implement sfx sliders
    public static AudioManager Instance;

    public Slider musicVolumeSlider;
    //public Slider sfxVolumeSlider;

    //private float musicVolume = 0.5f;
    //private float sfxVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // load saved volume settings or default to 0.5
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        // assigns value
        musicVolumeSlider.value = savedMusicVolume;
        //sfxSlider.value = savedSFXVolume;

        // sets volume
        SetMusicVolume(savedMusicVolume);
        //SetSFXVolume(savedSFXVolume);     // Add sfx later if

        // add listeners for changes
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        //sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // sets the volume 
        PlayerPrefs.Save();  // saves changes
    }
        /*
        public void SetSFXVolume(float volume)
        {      
            PlayerPrefs.SetFloat("SFXVolume", volume);
            PlayerPrefs.Save();
        }
        */
}