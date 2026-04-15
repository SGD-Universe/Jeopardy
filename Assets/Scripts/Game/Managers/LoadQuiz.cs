using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class LoadQuiz : MonoBehaviour
{

    public BoardLoadData LoadData = new BoardLoadData();

    public GameObject SaveObject;

    [Header("Default")]
    public bool fileImported = false;
    public string importQuizName = "";
    public string importFilePath = "";

    [System.Serializable]
    public class BoardLoadData
    {
        public string Title; //will also be quiz file name, so no worries on configuring it to imported
        public string[] Category = new string[6];
        public string[] Question = new string[30];
        public string[] Answer = new string[30];
    }
    void Start()
    {
        //set the SaveObject to whatever holds the 'SaveQuiz'
    }

    public void LoadSavedQuiz()
    {
        string filePath = importFilePath;
        string QuizLoadData = File.ReadAllText(importFilePath);

        LoadData = JsonUtility.FromJson<BoardLoadData>(QuizLoadData);
        Debug.Log(LoadData.Title + " has been loaded");

        //find objects with a tag that distinguishes the panels, or simply loaded data for them while proving the greenlight to take data from it.
    }


    //checks to see if a file has been imported before a the quiz edit screen is pulled up.
    public void DefaultEdit()
    {

        if (fileImported == true)
        {
            Debug.Log("Swap to Quiz Edit Screen");
        }

    }

    public void DefaultImport()
    {
        string localImport = "";

        if (importQuizName != null || importQuizName != "")
        {
            Debug.Log("Importing Quiz...");
            localImport = Application.persistentDataPath + "/QuizTemplates/" + importQuizName + ".json";
            if (File.Exists(localImport) == true)
            {
                importFilePath = localImport;
                fileImported = true;
                Debug.Log("Imported Quiz - " + importFilePath);
            }
            else
            {
                Debug.Log("Failed to find file path; ensure file name is correct");
            }
        }
        else
        {
            Debug.LogError("Please Input File Name Into Input Field");
        }

        // This will need file browser functionality later, probably

    }

    public void DefaultRead()
    {
        //importQuizName = importField.text;
        Debug.Log(importQuizName);
    }
}
