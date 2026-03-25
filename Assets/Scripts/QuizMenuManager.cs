using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMenuManager : MonoBehaviour
{
    public Button createQuizButton;
    public Button editQuizButton;
    public Button importQuizButton;
    public Button backButton;

    string createQuizSceneName = "test-creation-mode";
    string editQuizSceneName = "EditQuizScene";
    string mainMenuSceneName = "Main";

    void Start()
    {
        if (createQuizButton != null)
        {
            createQuizButton.onClick.AddListener(LoadCreateQuizScene);
        }

        if (editQuizButton != null)
        {
            editQuizButton.onClick.AddListener(LoadEditQuizScene);
        }

        if (importQuizButton != null)
        {
            importQuizButton.onClick.AddListener(ImportQuizFile);
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    public void LoadCreateQuizScene()
    {
        SceneManager.LoadScene(createQuizSceneName);
    }

    public void LoadEditQuizScene()
    {
        SceneManager.LoadScene(editQuizSceneName);
    }

    public void ImportQuizFile()
    {
        string targetApplication = "explorer.exe";
        string quizTemplateFolderPath = Application.persistentDataPath + "/QuizTemplates";

        UnityEngine.Debug.Log("Import Quiz File clicked - feature to be implemented");
        // This will need file browser functionality later

        // TODO: Open the File Explorer into the quiz template folder path when the respective button is pressed

        Process.Start(targetApplication, $"/select,\"" + quizTemplateFolderPath + "\""); // Open File Explorer to the quiz template folder in file select mode

        UnityEngine.Debug.Log(targetApplication + " opened to file path: " + quizTemplateFolderPath);

        // Opens the File Explorer, but does not take the player to the quiz templates folder and does not let the player to select a quiz template file
    }

    public void ReturnToMainMenu()
    {
        // BUG: Main menu music resets when exiting the Edit or Create Quiz menu

        SceneManager.LoadScene(mainMenuSceneName); // Likely the culprit of the music restarting when exiting
    }

    void OnDestroy()
    {
        if (createQuizButton != null)
        {
            createQuizButton.onClick.RemoveListener(LoadCreateQuizScene);
        }

        if (editQuizButton != null)
        {
            editQuizButton.onClick.RemoveListener(LoadEditQuizScene);
        }

        if (importQuizButton != null)
        {
            importQuizButton.onClick.RemoveListener(ImportQuizFile);
        }

        if (backButton != null)
        {
            backButton.onClick.RemoveListener(ReturnToMainMenu);
        }
    }
}
