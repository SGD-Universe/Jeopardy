using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;

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

    string quizTemplatePath;
    string savedGamePath;

    [Header("File Counts")]
    public int quizTemplateCount;
    public int savedGameCount;

    void Awake()
    {
        quizTemplatePath = Application.persistentDataPath + "/QuizTemplates";
        savedGamePath = Application.persistentDataPath + "/SavedGames";

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
        CreateQuizTemplateFolder(quizTemplatePath);
        CreateSavedGameFolder(savedGamePath);

        quizTemplateCount = CountQuizTemplateJsonFiles(quizTemplatePath);
        savedGameCount = CountSavedGameJsonFiles(savedGamePath);
    }

    // TODO: Move the trigger audio functions elsewhere.

    public void TriggerQuestionCorrect()
    {
        AudioManager.Instance.PlaySoundCorrect();
    }

    public void TriggerQuestionIncorrect()
    {
        AudioManager.Instance.PlaySoundIncorrect();
    }

    public void CreateQuizTemplateFolder(string quizTemplateFolderPath)
    {
        if (!Directory.Exists(quizTemplateFolderPath))
        {
            Directory.CreateDirectory(quizTemplateFolderPath);

            Debug.Log("Quiz template folder did not exist. Quiz template folder created!");
        }
        else
        {
            Debug.Log("Quiz template folder already exists!");
        }
    }

    public int CountQuizTemplateJsonFiles(string quizTemplateFolderPath)
    {
        string[] quizTemplateFiles = Directory.GetFiles(quizTemplateFolderPath, "*.json");

        Debug.Log($"Number of quiz template JSON files found: {quizTemplateFiles.Length}");

        return quizTemplateFiles.Length;
    }

    public void CreateSavedGameFolder(string savedGameFolderPath)
    {
        if (!Directory.Exists(savedGameFolderPath))
        {
            Directory.CreateDirectory(savedGameFolderPath);

            Debug.Log("Saved game folder did not exist. Saved game folder created!");
        }
        else
        {
            Debug.Log("Saved game folder already exists!");
        }
    }

    public int CountSavedGameJsonFiles(string savedGameFolderPath)
    {
        string[] savedGameFiles = Directory.GetFiles(savedGameFolderPath, "*.json");

        Debug.Log($"Number of saved game JSON files found: {savedGameFiles.Length}");

        return savedGameFiles.Length;
    }
}
