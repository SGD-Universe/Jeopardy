using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class SaveGame : MonoBehaviour
{
    public GameSaveData SaveData = new GameSaveData();

    public string savedQuizTitle;
    public string[] savedQuizCategory = new string[6];
    public string[] savedQuizQuestion = new string[30];
    public string[] savedQuizAnswer = new string[30];
    public bool[] isComplete = new bool[30];
    public int[] pointValue = new int[30];
    public bool[] isDailyDouble = new bool[30];

    [System.Serializable]
    public class GameSaveData
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

    void Start()
    {
        
    }


    public void SaveGameState()
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
