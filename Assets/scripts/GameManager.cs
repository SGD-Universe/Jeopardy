using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public enum QuizPlayMode
    {
        None,
        Quiz,
        Editor
    }

    public static GameManager Instance;

    public QuizPlayMode quizPlayMode;

    [Range(1, 3)]
    public int teamCount;

    public int teamOneScore;
    public int teamTwoScore;
    public int teamThreeScore;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TriggerQuestionCorrect()
    {
        AudioManager.Instance.PlaySoundCorrect();
    }

    public void TriggerQuestionIncorrect()
    {
        AudioManager.Instance.PlaySoundIncorrect();
    }
}
