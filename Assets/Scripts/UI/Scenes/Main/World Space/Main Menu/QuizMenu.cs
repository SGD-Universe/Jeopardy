using SFB;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuizMenu : MonoBehaviour
{
    public Button createQuizButton;
    public Button editQuizButton;
    public Button importQuizButton;

    void Start()
    {
        
    }

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
        string targetApplication = "explorer.exe";
        string quizTemplateFolderPath = Application.persistentDataPath + "/QuizTemplates";
        var paths = StandaloneFileBrowser.OpenFilePanel("Quizzes", Application.persistentDataPath + "/QuizTemplates", "json", false);
        //UnityEngine.Debug.Log("Import Quiz File clicked - feature to be implemented"); *Solved now needs to be connected with the jeopardy board
        // This will need file browser functionality later


        //Process.Start(targetApplication, $"/select,\"" + quizTemplateFolderPath + "\"");

        UnityEngine.Debug.Log(targetApplication + " opened to file path: " + quizTemplateFolderPath);

        
    }
}
