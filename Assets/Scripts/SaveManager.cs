using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
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
        string folderPath = Application.persistentDataPath + "/QuizTemplates";
        if(!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        string filePath = folderPath + "/" + fileName + ".json";
        File.WriteAllText(filePath, json);
        Process.Start("explorer.exe", "/select,\"" + Path.GetFullPath(filePath) + "\"");
        return 0;
    }
}
