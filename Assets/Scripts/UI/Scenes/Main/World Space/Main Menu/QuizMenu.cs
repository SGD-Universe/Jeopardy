using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuizMenu : MonoBehaviour
{
    public Button createQuizButton;
    public Button editQuizButton;
    public Button importQuizButton;

    [Header("UI Reference")]
    public LoadQuizzesMenu loadQuizzesMenu;

    void OnEnable()
    {
        if (createQuizButton != null)
        {
            createQuizButton.onClick.AddListener(LoadCreateQuizScene);
            UnityEngine.Debug.Log("QUIZ MENU ON ENABLE: LoadCreateQuizScene listener added!");
        }

        if (editQuizButton != null)
        {
            editQuizButton.onClick.AddListener(LoadEditQuizScene);
            UnityEngine.Debug.Log("QUIZ MENU ON ENABLE: LoadEditQuizScene listener added!");
        }

        if (importQuizButton != null)
        {
            importQuizButton.onClick.AddListener(ImportQuizFile);
            UnityEngine.Debug.Log("QUIZ MENU ON ENABLE: ImportQuizFile listener added!");
        }
    }

    void OnDisable()
    {
        if (createQuizButton != null)
        {
            createQuizButton.onClick.RemoveListener(LoadCreateQuizScene);
            UnityEngine.Debug.Log("QUIZ MENU ON DISABLE: LoadCreateQuizScene listener removed!");
        }

        if (editQuizButton != null)
        {
            editQuizButton.onClick.RemoveListener(LoadEditQuizScene);
            UnityEngine.Debug.Log("QUIZ MENU ON DISABLE: LoadEditQuizScene listener removed!");
        }

        if (importQuizButton != null)
        {
            importQuizButton.onClick.RemoveListener(ImportQuizFile);
            UnityEngine.Debug.Log("QUIZ MENU ON DISABLE: ImportQuizFile listener removed!");
        }
    }

    public void LoadCreateQuizScene()
    {
        SceneManager.LoadScene("test-creation-mode");
    }

    public void LoadEditQuizScene()
    {
        UnityEngine.Debug.LogError("The edit quiz Scene does not exist in the Scenes folder!");
    }

    public void ImportQuizFile()
    {
        string quizTemplateFolderPath = Application.streamingAssetsPath + "/QuizTemplates";
        if (!Directory.Exists(quizTemplateFolderPath))
        {
            Directory.CreateDirectory(quizTemplateFolderPath);
            UnityEngine.Debug.Log("Directory created");
        }

        // Refresh the scroll view with all quizzes in the folder
        if (loadQuizzesMenu != null)
        {
            loadQuizzesMenu.PopulateQuizList();
        }
        else
        {
            UnityEngine.Debug.LogError("LoadQuizzesMenu reference is missing on QuizMenu script!");
        }
    }
}
