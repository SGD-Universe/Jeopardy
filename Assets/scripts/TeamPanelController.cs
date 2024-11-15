using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour
{
    public string teamID; // Unique identifier for each team
    public TMP_Text teamNameText; // Text to display the team name
    public TMP_Text teamScoreText; // Text to display the team's score
    public Button addScoreButton; // Button to add score
    public Button subtractScoreButton; // Button to subtract score
    public TMP_InputField teamNameInputField; // Input field for the team name

    private Team team; // Reference to the Team object

    // Initialize team properties at the start
    private void Start()
    {
        // Set up listeners for the buttons and input field
        addScoreButton.onClick.AddListener(OnAddScoreClicked);
        subtractScoreButton.onClick.AddListener(OnSubtractScoreClicked);
        teamNameInputField.onEndEdit.AddListener(OnTeamNameInputEndEdit);

        // Load team data if teamID is set
        if (!string.IsNullOrEmpty(teamID))
        {
            LoadData();
        }

        // Update the UI to reflect the initial state
        UpdateUI();
    }

    // Assign a Team object and update the UI
    public void SetTeam(Team team)
    {
        this.team = team;
        teamID = team.teamID; // Assume Team object has a unique identifier
        UpdateUI();
    }

    // Update the UI to show the current team name and score
    private void UpdateUI()
    {
        if (team != null)
        {
            teamNameText.text = team.teamName; // Update displayed team name
            teamScoreText.text = "Score: " + team.score; // Update displayed score
            teamNameInputField.text = team.teamName; // Reflect the name in input field
        }
        else
        {
            teamNameText.text = teamName; // Display loaded name if no Team object
            teamScoreText.text = "Score: " + score;
            teamNameInputField.text = teamName;
        }
    }

    // Method called when the Add Score button is clicked
    private void OnAddScoreClicked()
    {
        if (team != null)
        {
            team.score += 100;
        }
        else
        {
            score += 100;
        }
        UpdateUI();
        SaveData();
    }

    // Method called when the Subtract Score button is clicked
    private void OnSubtractScoreClicked()
    {
        if (team != null)
        {
            team.score -= 100;
        }
        else
        {
            score -= 100;
        }
        UpdateUI();
        SaveData();
    }

    // Method called when the team name input is edited
    private void OnTeamNameInputEndEdit(string newName)
    {
        if (team != null)
        {
            team.teamName = newName;
        }
        else
        {
            teamName = newName;
        }
        SaveData();
        UpdateUI();
    }

    // Save score and team name using PlayerPrefs
    private void SaveData()
    {
        if (!string.IsNullOrEmpty(teamID))
        {
            PlayerPrefs.SetInt("TeamScore_" + teamID, team != null ? team.score : score);
            PlayerPrefs.SetString("TeamName_" + teamID, team != null ? team.teamName : teamName);
            PlayerPrefs.Save();
        }
    }

    // Load score and team name from PlayerPrefs
    private void LoadData()
    {
        score = PlayerPrefs.GetInt("TeamScore_" + teamID, 0);
        teamName = PlayerPrefs.GetString("TeamName_" + teamID, "Team");
        if (team != null)
        {
            team.score = score;
            team.teamName = teamName;
        }
    }
}
