using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuizMenuManager : MonoBehaviour
{
    public Button createQuizButton;
    public Button editQuizButton;
    public Button importQuizButton;
    public Button backButton;
    public TMP_InputField importField;

    string createQuizSceneName = "test-creation-mode";
    string editQuizSceneName = "EditQuizScene";
    string mainMenuSceneName = "Main";
    string importQuizName = "";
    string importedFilePath = "";

    bool fileImported = false;

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
        importQuizName = importField.text;
    }

    public void LoadCreateQuizScene()
    {
        string[] quizTemplates = Directory.GetFiles(importedFilePath);

        if (quizTemplates.Length == 0)
        {
            UnityEngine.Debug.LogError("No Quiz Templates Found");
            //return new BoardData();
        }

        string json = File.ReadAllText(quizTemplates[0]);
        //BoardData boardData = JsonUtility.FromJson<BoardData>(json);
        //return boardData;
        SceneManager.LoadScene(createQuizSceneName);
    }

    public void LoadEditQuizScene()
    {
        if (fileImported == true)
        {
            SceneManager.LoadScene(editQuizSceneName);
            //will need to load the usual quiz creation scene, but with all information related to the quiz placed in the correct areas
            //Basically just replacing the standard placeholder text with the text from the quiz questions
        }
    }

    public void ImportQuizFile()
    {
        string localImport = "";
        
        if (importQuizName != null || importQuizName != "")
        {
            Debug.Log("Importing Quiz...");
            localImport = Application.persistentDataPath + "/QuizTemplates/" + importQuizName + ".json";
            if (File.Exists(localImport) == true)
            {
                importedFilePath = localImport;
                fileImported = true;
                Debug.Log("Imported Quiz - " + importedFilePath);
            }
            else
            {
                Debug.Log("failed to find file path; ensure file name is correct");
            }
        }
        else
        {
            Debug.LogError("Please Input File Name Into Input Field");
        }

        // This will need file browser functionality later
    }

    public void ReadImportText()
    {
        importQuizName = importField.text;
        Debug.Log(importQuizName);
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
