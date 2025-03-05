using UnityEngine;

public class ExperimentalAudioManager : MonoBehaviour
{
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
    //If you change or remove any sound files (music or sfx), change or remove the above clips appropriately.



    private void Start()
    {
        musicSource.clip = MusMainMenu;
        musicSource.Play();
    }
}
