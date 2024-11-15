// File: GameManager.cs

using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; // Required for File, Path, and Directory operations

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [System.Serializable]
    public class UIState
    {
        public string[] teamNames;    // Store names of the teams
        public int[] teamScores;      // Store scores of the teams
        public string[] categories;   // Store an array of categories (e.g., 5 categories)
        public string saveFileName;   // Store the save file name (e.g., "Save 1")
    }

    public UIState currentState;
    private string saveDirectory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveDirectory = Application.persistentDataPath;  // Persistent save directory
        }
        else
        {
            Destroy(gameObject);  // Ensure only one GameManager exists
        }
    }

    // Load a scene by name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Save the current UI state to a JSON file
    public void SaveUIState(string saveFileName, string[] categories)
    {
        var gameSceneController = FindFirstObjectByType<GameSceneController>();  // Replaced deprecated method
        if (gameSceneController != null)
        {
            var teamPanels = gameSceneController.GetComponentsInChildren<TeamPanelController>();
            currentState = new UIState
            {
                teamNames = new string[teamPanels.Length],
                teamScores = new int[teamPanels.Length],
                categories = categories,
                saveFileName = saveFileName
            };

            for (int i = 0; i < teamPanels.Length; i++)
            {
                currentState.teamNames[i] = teamPanels[i].teamNameInputField.text;
                currentState.teamScores[i] = int.Parse(teamPanels[i].teamScoreText.text.Replace("Score: ", ""));
            }

            string json = JsonUtility.ToJson(currentState);
            string filePath = Path.Combine(saveDirectory, saveFileName + ".json");

            File.WriteAllText(filePath, json);
            Debug.Log($"Game saved to: {filePath}");
        }
    }

    // Load a saved UI state from a JSON file
    public void LoadUIState(string saveFileName)
    {
        string filePath = Path.Combine(saveDirectory, saveFileName + ".json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            currentState = JsonUtility.FromJson<UIState>(json);

            var gameSceneController = FindFirstObjectByType<GameSceneController>();
            if (gameSceneController != null)
            {
                var teamPanels = gameSceneController.GetComponentsInChildren<TeamPanelController>();

                for (int i = 0; i < teamPanels.Length; i++)
                {
                    if (i < currentState.teamNames.Length && i < currentState.teamScores.Length)
                    {
                        teamPanels[i].teamNameText.text = currentState.teamNames[i];
                        teamPanels[i].teamNameInputField.text = currentState.teamNames[i];
                        teamPanels[i].teamScoreText.text = "Score: " + currentState.teamScores[i];
                    }
                }

                Debug.Log($"UI State Loaded from: {filePath}");
            }
        }
        else
        {
            Debug.LogError("No saved file found at: " + filePath);
        }
    }

    // Get all save files in the save directory
    public string[] GetSaveFiles()
    {
        if (Directory.Exists(saveDirectory))
        {
            return Directory.GetFiles(saveDirectory, "*.json");
        }
        return new string[0];
    }

    // Quit the game (only works in a build, not in editor)
    public void QuitGame()
    {
        Application.Quit();
    }
}
