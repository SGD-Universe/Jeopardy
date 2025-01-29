using UnityEngine;
using UnityEngine.UI;

// ## Class needs refactoring
// Due to the gameobject being set to DontDestroyOnLoad, the reference to musicVolumeSlider is lost
// when the scene it starts in (MainMenu) is unloaded.
// Additionally, this class manipulates the static property 'volume' from the AudioListener class,
// which is the global volume of all sounds in the game. This should be changed to use Unity's Audio Mixers,
// which allows mixing multiple categories of sounds at the same time
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Slider musicVolumeSlider;
    //public Slider sfxVolumeSlider;

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
        // If there is no saved volume setting, default to 0.5
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        musicVolumeSlider.value = savedMusicVolume;
        //sfxSlider.value = savedSFXVolume;

        SetMusicVolume(savedMusicVolume);
        //SetSFXVolume(savedSFXVolume);

        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        //sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    /*
    public void SetSFXVolume(float volume)
    {      
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
    */
}