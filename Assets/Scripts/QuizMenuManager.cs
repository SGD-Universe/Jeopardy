using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Import Quiz File clicked - feature to be implemented");
        // This will need file browser functionality later
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
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
