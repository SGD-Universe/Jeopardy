using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum QuizPlayMode
    {
        None,
        Quiz,
        Editor
    }

    public QuizPlayMode quizPlayMode;

    [Range(1, 3)]
    public int teamCount;

    public int teamOneScore;
    public int teamTwoScore;
    public int teamThreeScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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