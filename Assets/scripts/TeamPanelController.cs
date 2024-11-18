using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour
{
    public string teamID; // Unique identifier for each team
    public string teamName;
    public int score;

    public TMP_Text teamNameText; // Text to display the team name
    public TMP_Text teamScoreText; // Text to display the team's score
    public Button addScoreButton; // Button to add score
    public Button subtractScoreButton; // Button to subtract score
    public TMP_InputField teamNameInputField; // Input field for the team name

    private void Start()
    {
        // Set up listeners for score buttons and input field
        addScoreButton.onClick.AddListener(OnAddScoreClicked);
        subtractScoreButton.onClick.AddListener(OnSubtractScoreClicked);
        teamNameInputField.onEndEdit.AddListener(OnTeamNameInputEndEdit);

        UpdateUI();
    }

    // Updates the UI elements with the current data
    public void UpdateUI()
    {
        teamNameText.text = teamName;
        teamScoreText.text = "Score: " + score;
        teamNameInputField.text = teamName;
    }

    // Assigns team data
    public void SetTeam(string name, int teamScore)
    {
        teamName = name;
        score = teamScore;
        UpdateUI();
    }

    // Updates team name via input
    private void OnTeamNameInputEndEdit(string newName)
    {
        teamName = newName;
        UpdateUI();
    }

    // Adds to the team's score
    private void OnAddScoreClicked()
    {
        score += 100;
        UpdateUI();
    }

    // Subtracts from the team's score
    private void OnSubtractScoreClicked()
    {
        score -= 100;
        UpdateUI();
    }
}
