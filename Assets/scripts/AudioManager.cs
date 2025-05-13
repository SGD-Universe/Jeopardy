using UnityEngine;
using UnityEngine.Audio;

//Attached on GameObject: AudioManager

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Mixer -----")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----- Audio Clips -----")]
    public AudioClip Buzzer;
    public AudioClip Correct;
    public AudioClip Incorrect;
    public AudioClip IncorrectBassBoosted;
    public AudioClip MusEditor;
    public AudioClip MusJeopardyTheme;
    public AudioClip MusMainMenu;
    public AudioClip MusQuizGame;
    public AudioClip QuestionReveal;
    public AudioClip QuizIntro;
    public AudioClip WinClapping;
    //If you change or remove any sound files (music or sfx), please change or remove the above clips appropriately.



    private void Start()
    {
        // Load volume preferences - Default value is 0.5f
        float musicVol = PlayerPrefs.GetFloat("musicVol", 0.5f);
        float sfxVol = PlayerPrefs.GetFloat("sfxVol", 0.5f);
        audioMixer.SetFloat("musicVol", Mathf.Log10(musicVol) * 20);
        audioMixer.SetFloat("sfxVol", Mathf.Log10(sfxVol) * 20);

        musicSource.clip = MusMainMenu;
        musicSource.Play();
    }
}
