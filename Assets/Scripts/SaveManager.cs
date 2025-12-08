using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;

    [Header("Debug / Testing")]
    [Tooltip("If true, scores will auto-increase every frame for testing.")]
    [SerializeField] private bool testMode = false;

    private static string quizTemplateFolderPath;
    private static string saveScoreFilePath;

    // Holds score data for JSON file saving
    public static TeamScoringData teamScoring = new TeamScoringData();

    #region Unity Lifecycle

    private void Awake()
    {
        InitPaths();

        if (teamScoring == null)
            teamScoring = new TeamScoringData();
    }

    private void Start()
    {
        // Optional: try loading saved scores on start
        // LoadGame();  // Uncomment if you want automatic load
    }

    private void Update()
    {
        if (!testMode || gameManager == null)
            return;

        // For testing and showcase purposes, shows scores and auto-increments
        UnityEngine.Debug.Log(Mathf.Round(gameManager.teamOneScore));
        UnityEngine.Debug.Log(Mathf.Round(gameManager.teamTwoScore));
        UnityEngine.Debug.Log(Mathf.Round(gameManager.teamThreeScore));

        gameManager.teamOneScore += 1f * Time.deltaTime;
        gameManager.teamTwoScore += 2f * Time.deltaTime;
        gameManager.teamThreeScore += 3f * Time.deltaTime;

        teamScoring.teamOneScore = gameManager.teamOneScore;
        teamScoring.teamTwoScore = gameManager.teamTwoScore;
        teamScoring.teamThreeScore = gameManager.teamThreeScore;
    }

    #endregion

    #region Data Classes

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
    public class TeamScoringData
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
            while (column > columns.Count - 1)
            {
                columns.Add(new ColumnData());
            }

            columns[column].panels.Add(new PanelData(isCategory, primaryText, secondaryText));
        }
    }

    #endregion

    #region Path / Filename Helpers

    private static void InitPaths()
    {
        if (string.IsNullOrEmpty(saveScoreFilePath))
        {
            saveScoreFilePath = Path.Combine(Application.persistentDataPath, "TeamScoringData.json");
        }

        if (string.IsNullOrEmpty(quizTemplateFolderPath))
        {
            quizTemplateFolderPath = Path.Combine(Application.persistentDataPath, "QuizTemplates");
        }
    }

    /// <summary>
    /// Verifies that a file name is non-empty and has no invalid characters.
    /// Returns:
    ///  0  = OK
    /// -1  = Null/empty/whitespace
    /// -2  = Contains invalid characters
    /// </summary>
    public static int VerifyFileName(string fileName)
    {
        char[] invalid = Path.GetInvalidFileNameChars();

        if (string.IsNullOrWhiteSpace(fileName))
            return -1;

        foreach (char c in fileName)
        {
            if (System.Array.IndexOf(invalid, c) >= 0)
                return -2;
        }

        return 0;
    }

    #endregion

    #region Board Save / Load

    public static int SaveBoardData(BoardData boardData, string fileName)
    {
        InitPaths();

        int verifyResult = VerifyFileName(fileName);
        if (verifyResult != 0)
            return verifyResult;

        // Ensure folder exists
        if (!Directory.Exists(quizTemplateFolderPath))
        {
            Directory.CreateDirectory(quizTemplateFolderPath);
        }

        string quizTemplateFilePath = Path.Combine(quizTemplateFolderPath, fileName + ".json");
        string json = JsonUtility.ToJson(boardData, true);
        File.WriteAllText(quizTemplateFilePath, json);

        // Open explorer to show file (Windows only)
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        Process.Start("explorer.exe", "/select,\"" + Path.GetFullPath(quizTemplateFilePath) + "\"");
#endif

        return 0;
    }

    public static BoardData LoadRandomBoardData()
    {
        InitPaths();

        if (!Directory.Exists(quizTemplateFolderPath))
        {
            UnityEngine.Debug.LogError("Quiz template folder not found: " + quizTemplateFolderPath);
            return new BoardData();
        }

        string[] quizTemplates = Directory.GetFiles(quizTemplateFolderPath, "*.json");

        if (quizTemplates == null || quizTemplates.Length == 0)
        {
            UnityEngine.Debug.LogError("No Quiz Templates Found in: " + quizTemplateFolderPath);
            return new BoardData();
        }

        // TODO: randomize if you want a random one
        string json = File.ReadAllText(quizTemplates[0]);
        BoardData boardData = JsonUtility.FromJson<BoardData>(json);
        return boardData ?? new BoardData();
    }

    #endregion

    #region Score Save / Load

    public void SaveGame()
    {
        InitPaths();

        if (gameManager == null)
        {
            UnityEngine.Debug.LogError("SaveManager: GameManager reference is missing.");
            return;
        }

        teamScoring.teamOneScore = gameManager.teamOneScore;
        teamScoring.teamTwoScore = gameManager.teamTwoScore;
        teamScoring.teamThreeScore = gameManager.teamThreeScore;

        string teamScoringData = JsonUtility.ToJson(teamScoring, true);
        UnityEngine.Debug.Log("Saving scores to: " + saveScoreFilePath);

        File.WriteAllText(saveScoreFilePath, teamScoringData);
        UnityEngine.Debug.Log("Scores saved.");
    }

    public void LoadGame()
    {
        InitPaths();

        if (!File.Exists(saveScoreFilePath))
        {
            UnityEngine.Debug.LogWarning("No save file found at: " + saveScoreFilePath + ". Initializing scores to 0.");
            teamScoring = new TeamScoringData();

            if (gameManager != null)
            {
                gameManager.teamOneScore = 0f;
                gameManager.teamTwoScore = 0f;
                gameManager.teamThreeScore = 0f;
            }

            return;
        }

        string teamScoringData = File.ReadAllText(saveScoreFilePath);
        teamScoring = JsonUtility.FromJson<TeamScoringData>(teamScoringData);

        if (teamScoring == null)
        {
            UnityEngine.Debug.LogError("Failed to parse TeamScoringData. Resetting scores.");
            teamScoring = new TeamScoringData();
        }

        if (gameManager != null)
        {
            gameManager.teamOneScore = teamScoring.teamOneScore;
            gameManager.teamTwoScore = teamScoring.teamTwoScore;
            gameManager.teamThreeScore = teamScoring.teamThreeScore;
        }

        UnityEngine.Debug.Log("Scores loaded.");
    }

    #endregion
}
