using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string sceneName;
    public Dictionary<string, List<string>> answeredQuestions = new Dictionary<string, List<string>>();
}

public class SaveManager : MonoBehaviour
{
    private static string saveDirectory;

    private void Awake()
    {
        saveDirectory = Application.persistentDataPath + "/Saves/";
        
        // Ensure the directory exists
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public static void SaveGame(string saveName)
    {
        SaveData data = new SaveData();
        data.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        data.answeredQuestions = GameManager.Instance.answeredQuestionsByCategory;

        string json = JsonUtility.ToJson(data, true);
        string savePath = saveDirectory + saveName + ".json";

        File.WriteAllText(savePath, json);
        Debug.Log($"Game saved: {savePath}");
    }

    public static void LoadGame(string saveName)
    {
        string savePath = saveDirectory + saveName + ".json";

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            GameManager.Instance.answeredQuestionsByCategory = data.answeredQuestions;
            GameManager.Instance.LoadSceneByName(data.sceneName);
            Debug.Log($"Game loaded: {saveName}");
        }
        else
        {
            Debug.LogError($"Save file not found: {savePath}");
        }
    }

    public static List<string> GetSavedGames()
    {
        List<string> savedGames = new List<string>();

        if (!Directory.Exists(saveDirectory))
        {
            return savedGames;
        }

        string[] files = Directory.GetFiles(saveDirectory, "*.json");
        foreach (string file in files)
        {
            savedGames.Add(Path.GetFileNameWithoutExtension(file));
        }

        return savedGames;
    }
}
