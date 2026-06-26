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

    public bool quizLoaded = false;
    public bool fileImported = false;
    public string importQuizName = "";
    public string importFilePath = "";

    // --- Data classes that mirror the JSON structure ---

    [System.Serializable]
    public class PanelData
    {
        public bool isCategory;
        public string primaryText;
        public string secondaryText;
    }

    [System.Serializable]
    public class ColumnData
    {
        public PanelData[] panels;
    }

    [System.Serializable]
    public class BoardLoadData
    {
        // Fixed board dimensions — must match OverviewScreen constants
        public const int MAX_CATEGORIES = 6;
        public const int MAX_QUESTIONS_PER_CATEGORY = 5;
        public const int MAX_QUESTIONS = MAX_CATEGORIES * MAX_QUESTIONS_PER_CATEGORY; // 30

        // This is what JsonUtility reads from the JSON file.
        public ColumnData[] columns;

        // --- Backward-compatible accessors ---
        // GamePanel.cs uses Category[panelNumb], Question[panelNumb], Answer[panelNumb].
        // These properties extract that data from the new columns structure
        // so nothing else in the project needs to change.
        // Arrays are ALWAYS fixed-size (6 categories, 30 questions, 30 answers).

        /// <summary>
        /// Always returns string[6] — one category name per column.
        /// Category name = the primaryText of the first panel in each column.
        /// </summary>
        public string[] Category
        {
            get
            {
                string[] cats = new string[MAX_CATEGORIES];
                for (int i = 0; i < MAX_CATEGORIES; i++)
                {
                    if (columns != null && i < columns.Length
                        && columns[i].panels != null && columns[i].panels.Length > 0)
                        cats[i] = columns[i].panels[0].primaryText;
                    else
                        cats[i] = "";
                }
                return cats;
            }
        }

        /// <summary>
        /// Always returns string[30] — questions in row-major order (left-to-right, top-to-bottom).
        /// Index = row * 6 + column, matching how OverviewScreen creates panels.
        /// </summary>
        public string[] Question
        {
            get
            {
                string[] questions = new string[MAX_QUESTIONS];
                for (int row = 0; row < MAX_QUESTIONS_PER_CATEGORY; row++)
                {
                    for (int col = 0; col < MAX_CATEGORIES; col++)
                    {
                        int panelIndex = row + 1; // +1 to skip the category header
                        int flatIndex = row * MAX_CATEGORIES + col;
                        if (columns != null && col < columns.Length
                            && columns[col].panels != null && panelIndex < columns[col].panels.Length)
                            questions[flatIndex] = columns[col].panels[panelIndex].primaryText;
                        else
                            questions[flatIndex] = "";
                    }
                }
                return questions;
            }
        }

        /// <summary>
        /// Always returns string[30] — answers in the same row-major order as Question.
        /// </summary>
        public string[] Answer
        {
            get
            {
                string[] answers = new string[MAX_QUESTIONS];
                for (int row = 0; row < MAX_QUESTIONS_PER_CATEGORY; row++)
                {
                    for (int col = 0; col < MAX_CATEGORIES; col++)
                    {
                        int panelIndex = row + 1;
                        int flatIndex = row * MAX_CATEGORIES + col;
                        if (columns != null && col < columns.Length
                            && columns[col].panels != null && panelIndex < columns[col].panels.Length)
                            answers[flatIndex] = columns[col].panels[panelIndex].secondaryText;
                        else
                            answers[flatIndex] = "";
                    }
                }
                return answers;
            }
        }
    }

    void Start()
    {
        
    }

    public void LoadSavedQuiz()
    {
        string QuizLoadData = File.ReadAllText(importFilePath);

        LoadData = JsonUtility.FromJson<BoardLoadData>(QuizLoadData);

        if (LoadData.columns != null)
        {
            quizLoaded = true;
            Debug.Log("Quiz loaded successfully! Columns: " + LoadData.columns.Length);
        }
        else
        {
            Debug.LogError("Failed to parse quiz JSON — 'columns' is null. Check the JSON file format.");
        }
    }

    //checks to see if a file has been imported before a the quiz edit screen is pulled up.
    public void DefaultEdit()
    {

        if (fileImported == true)
        {
            LoadSavedQuiz();
            quizLoaded = true;
        }
        else
        {
            quizLoaded = false;
        }

    }

    public void DefaultImport()
    {
        string localImport = "";

        if (importQuizName != null && importQuizName != "")
        {
            Debug.Log("Importing Quiz...");
            localImport = Application.streamingAssetsPath + "/QuizTemplates/" + importQuizName + ".json";
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
