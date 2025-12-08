using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Team Scores")]
    public float teamOneScore;
    public float teamTwoScore;
    public float teamThreeScore;

    [Header("Scoring Settings")]
    [Tooltip("Base points added or removed when a question is answered.")]
    [SerializeField] private int baseQuestionValue = 100;

    [Tooltip("Which team is currently answering? 0 = Team 1, 1 = Team 2, 2 = Team 3.")]
    [SerializeField][Range(0, 2)] private int activeTeamIndex = 0;

    [Tooltip("If true, incorrect answers subtract points. If false, they do not change the score.")]
    [SerializeField] private bool penalizeIncorrectAnswers = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // optional, but helpful if you change scenes
    }

    /// <summary>
    /// Called from MonitorPlane when Enter is pressed on a fullscreen question.
    /// </summary>
    public void TriggerQuestionCorrect()
    {
        AddScoreToActiveTeam(baseQuestionValue);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySoundCorrect();
        }

        Debug.Log($"[GameManager] Team {activeTeamIndex + 1} answered correctly. " +
                  $"Scores: T1={teamOneScore}, T2={teamTwoScore}, T3={teamThreeScore}");
    }

    /// <summary>
    /// Called from MonitorPlane when Delete is pressed on a fullscreen question.
    /// </summary>
    public void TriggerQuestionIncorrect()
    {
        if (penalizeIncorrectAnswers)
        {
            AddScoreToActiveTeam(-baseQuestionValue);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySoundIncorrect();
        }

        Debug.Log($"[GameManager] Team {activeTeamIndex + 1} answered incorrectly. " +
                  $"Scores: T1={teamOneScore}, T2={teamTwoScore}, T3={teamThreeScore}");
    }

    /// <summary>
    /// Set which team is currently answering. Hook this to UI buttons if you want Team 1/2/3 selectors.
    /// </summary>
    public void SetActiveTeam(int teamIndex)
    {
        activeTeamIndex = Mathf.Clamp(teamIndex, 0, 2);
        Debug.Log($"[GameManager] Active team is now Team {activeTeamIndex + 1}");
    }

    /// <summary>
    /// Optional helper to reset all scores.
    /// </summary>
    public void ResetScores()
    {
        teamOneScore = 0f;
        teamTwoScore = 0f;
        teamThreeScore = 0f;
    }

    private void AddScoreToActiveTeam(int delta)
    {
        switch (activeTeamIndex)
        {
            case 0:
                teamOneScore += delta;
                break;
            case 1:
                teamTwoScore += delta;
                break;
            case 2:
                teamThreeScore += delta;
                break;
        }
    }
}
