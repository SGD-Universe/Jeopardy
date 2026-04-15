using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveQuiz : MonoBehaviour
{

    public BoardSaveData SaveData = new BoardSaveData();

    //let these variables be accessed by the GamePanel Script and use the provided input for the save data
    public string savedQuizTitle;
    public string[] savedQuizCategory = new string[6];
    public string[] savedQuizQuestion = new string[30];
    public string[] savedQuizAnswer = new string[30];

    [System.Serializable]
    public class BoardSaveData
    {
        public string Title; //will also be quiz file name, so no worries on configuring it to imported
        public string[] Category = new string[6];
        public string[] Question = new string[30];
        public string[] Answer = new string[30];
    }

    void Start()
    {
        
    }

    public void SaveNewQuiz()
    {
        SaveData.Title = savedQuizTitle; //replace with quiz title input
        string fileName = savedQuizTitle;

        for (int i = 0; i < 6; i++)
        {
            SaveData.Category[i] = savedQuizCategory[i]; //replace with each game panel under the categories type
        }
        for (int j = 0; j < 30; j++)
        {
            SaveData.Question[j] = savedQuizQuestion[j];
            SaveData.Answer[j] = savedQuizAnswer[j];
        }

        string QuizSaveData = JsonUtility.ToJson(SaveData);
        string filePath = Application.persistentDataPath + "/" + fileName + ".json";
        File.WriteAllText(filePath, QuizSaveData);
    }
}
