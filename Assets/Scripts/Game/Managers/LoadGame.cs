using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class LoadGame : MonoBehaviour
{

    public GameLoadData LoadData = new GameLoadData();

    public bool quizLoaded = false;
    public bool fileImported = false;
    public string importQuizName = "";
    public string importFilePath = "";

    [System.Serializable]
    public class GameLoadData
    {
        public string Title;
        public string[] Category = new string[6];
        public string[] Question = new string[30];
        public string[] Answer = new string[30];
        public bool[] Completed = new bool[30];
        public int[] Value = new int[30]; //might not be neccessary
        public bool[] DDouble = new bool[30];
        public int TeamCount = 0;
        public int Team_1_Score = 0;
        public int Team_2_Score = 0;
        public int Team_3_Score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadSavedQuiz()
    {
        string filePath = importFilePath;
        string QuizLoadData = File.ReadAllText(importFilePath);

        LoadData = JsonUtility.FromJson<GameLoadData>(QuizLoadData);
        Debug.Log(LoadData.Title + " has been loaded");

    }

    public void DefaultLoad()
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
}
