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
        }
        else
        {
            Destroy(gameObject);
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep GameManager alive between scenes
            saveDirectory = Application.persistentDataPath;  // Persistent save directory
        }
        else
        {
            Destroy(gameObject);  // Ensure there's only one GameManager
        }
    }

    // loads the scene by name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

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
                categories = categories,        // Store the array of categories
                saveFileName = saveFileName     // Store the save file name (e.g., "Save 1")
            };

            for (int i = 0; i < teamPanels.Length; i++)
            {
                currentState.teamNames[i] = teamPanels[i].teamNameInputField.text;  // Save the input field's text
                currentState.teamScores[i] = int.Parse(teamPanels[i].teamScoreText.text.Replace("Score: ", ""));
            }

            string json = JsonUtility.ToJson(currentState);
            string filePath = Path.Combine(saveDirectory, saveFileName + ".json");  // Save as JSON file using saveFileName

            File.WriteAllText(filePath, json);  // Save the JSON data to a file
            Debug.Log($"Game saved to: {filePath}");  // Debug log to check save path
        }
    }

    public void LoadUIState(string saveFileName)
    {
        string filePath = Path.Combine(saveDirectory, saveFileName + ".json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            currentState = JsonUtility.FromJson<UIState>(json);  // Load the saved data

            var gameSceneController = FindFirstObjectByType<GameSceneController>();  // Replaced deprecated method
            if (gameSceneController != null)
            {
                var teamPanels = gameSceneController.GetComponentsInChildren<TeamPanelController>();

                for (int i = 0; i < teamPanels.Length; i++)
                {
                    if (i < currentState.teamNames.Length && i < currentState.teamScores.Length)
                    {
                        // Restore both teamNameText and teamNameInputField
                        teamPanels[i].teamNameText.text = currentState.teamNames[i];
                        teamPanels[i].teamNameInputField.text = currentState.teamNames[i];
                        teamPanels[i].teamScoreText.text = "Score: " + currentState.teamScores[i];
                    }
                }

                Debug.Log($"UI State Loaded from: {filePath}");  // Debug log to confirm loading
            }
        }
        else
        {
            Debug.LogError("No saved file found at: " + filePath);  // Error if file doesn't exist
        }
    }

    public string[] GetSaveFiles()
    {
        if (Directory.Exists(saveDirectory))
        {
            // Get all JSON files in the save directory
            return Directory.GetFiles(saveDirectory, "*.json");
        }
        return new string[0]; // Return an empty array if no files are found
    }

    // note: this will only work in the game, not in editor
    public void QuitGame()
    {
        Application.Quit();
    }
}
