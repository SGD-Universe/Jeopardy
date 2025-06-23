using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameManager gameManager;
    private static string quizTemplateFolderPath;
    private static string saveScoreFilePath;
    public static TeamScoringData teamScoring = new TeamScoringData();

    private void Start()
    {

        saveScoreFilePath = Application.persistentDataPath + "/TeamScoringData.json";
        quizTemplateFolderPath = Application.persistentDataPath + "/QuizTemplates";
        teamScoring = new TeamScoringData();
        
    }

    private void Update()
    {

        UnityEngine.Debug.Log(Mathf.Round(gameManager.teamOneScore)); //For testing and showcase purposes, shows scores every frame
        UnityEngine.Debug.Log(Mathf.Round(gameManager.teamTwoScore));
        UnityEngine.Debug.Log(Mathf.Round(gameManager.teamThreeScore));
        gameManager.teamOneScore += 1 * Time.deltaTime;
        gameManager.teamTwoScore += 2 * Time.deltaTime;
        gameManager.teamThreeScore += 3 * Time.deltaTime;
        teamScoring.teamOneScore = gameManager.teamOneScore;
        teamScoring.teamTwoScore = gameManager.teamTwoScore;
        teamScoring.teamThreeScore = gameManager.teamThreeScore;

    }


    [System.Serializable]
    public class PanelData
    {
        public bool isCategory;
        public string primaryText;
        public string secondaryText;

        public PanelData(bool isCategory, string primaryText, string secondaryText)
        {
            this.isCategory = isCategory;
            this.primaryText = primaryText;
            this.secondaryText = secondaryText;
        }
    }

    [System.Serializable]
    public class TeamScoringData //Holds score data for JSON file saving
    {
        
        public float teamOneScore;
        public float teamTwoScore;
        public float teamThreeScore;

    }

    [System.Serializable]
    public class ColumnData
    {
        public List<PanelData> panels;
        public ColumnData()
        {
            panels = new List<PanelData>();
        }
    }

    [System.Serializable]
    public class BoardData
    {
        public List<ColumnData> columns;
        public BoardData()
        {
            columns = new List<ColumnData>();
        }

        public void AddPanel(int column, bool isCategory, string primaryText, string secondaryText)
        {
            while(column > columns.Count() - 1) columns.Add(new ColumnData());
            columns[column].panels.Add(new PanelData(isCategory, primaryText, secondaryText));
        }
    }

    public static int VerifyFileName(string fileName)
    {
        char[] invalid = Path.GetInvalidFileNameChars();

        if(string.IsNullOrWhiteSpace(fileName)) return -1;

        foreach(char c in fileName)
        {
            if(invalid.Contains(c)) return -2;
        }

        return 0;
    }

    public static int SaveBoardData(BoardData boardData, string fileName)
    {
        // Verify File Name
        char[] invalid = Path.GetInvalidFileNameChars();

        if(string.IsNullOrWhiteSpace(fileName)) return -1;

        foreach(char c in fileName)
        {
            if(invalid.Contains(c)) return -2;
        }

        // Save Data to file
        string json = JsonUtility.ToJson(boardData, true);
        if(!Directory.Exists(quizTemplateFolderPath)) Directory.CreateDirectory(quizTemplateFolderPath);
        string quizTemplatefilePath = quizTemplateFolderPath + "/" + fileName + ".json";
        File.WriteAllText(quizTemplatefilePath, json);
        Process.Start("explorer.exe", "/select,\"" + Path.GetFullPath(quizTemplatefilePath) + "\"");
        return 0;
    }

    public static BoardData LoadRandomBoardData()
    {
        string[] quizTemplates = Directory.GetFiles(quizTemplateFolderPath, "*.json");
        
        if(quizTemplates.Length == 0)
        {
            Warning.Caution("No Quiz Templates Found");
            return new BoardData();
        }

        string json = File.ReadAllText(quizTemplates[0]);
        BoardData boardData = JsonUtility.FromJson<BoardData>(json);
        return boardData;
    }

    public void SaveGame() //Saves game data (Currently just team scores)
    {

        teamScoring.teamOneScore = gameManager.teamOneScore; //Ensures scores are saved properly
        teamScoring.teamTwoScore = gameManager.teamTwoScore;
        teamScoring.teamThreeScore = gameManager.teamThreeScore;
        string teamScoringData = JsonUtility.ToJson(teamScoring); //Saves JSON formatted scores to a string
        UnityEngine.Debug.Log(saveScoreFilePath); //Displays file path in debug log
        System.IO.File.WriteAllText(saveScoreFilePath, teamScoringData); //Writes JSON formatted string to the file path specified in the saveScoreFilePath variable
        UnityEngine.Debug.Log("Scores saved."); //Displays "Scores saved."

    }

    public void LoadGame()
    {

        string teamScoringData = System.IO.File.ReadAllText(saveScoreFilePath); //Sets string to the text found in the JSON file
        teamScoring = JsonUtility.FromJson<TeamScoringData>(teamScoringData); //Converts it to floats
        gameManager.teamOneScore = teamScoring.teamOneScore; //Sets all team scores to what they are in the save file
        gameManager.teamTwoScore = teamScoring.teamTwoScore;
        gameManager.teamThreeScore = teamScoring.teamThreeScore;
        UnityEngine.Debug.Log("Scores loaded."); //Displays "Scores loaded." in the debug log

    }

}
