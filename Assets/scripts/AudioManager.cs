using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

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

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Load volume preferences - Default value is 0.5f
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);

        musicSource.clip = MusMainMenu;
        musicSource.Play();
    }

    public void PlaySoundCorrect()
    {
        sfxSource.PlayOneShot(Correct);
    }

    public void PlaySoundIncorrect()
    {
        sfxSource.PlayOneShot(Incorrect);
    }
}
