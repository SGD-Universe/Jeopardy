using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour
{
    public TMP_Text teamNameText;            // Text to display the team name
    public TMP_Text teamScoreText;           // Text to display the team's score
    public Button addScoreButton;            // Button to add score
    public Button subtractScoreButton;       // Button to subtract score
    public TMP_InputField teamNameInputField; // Input field for the team name

    private Team team;  // Reference to the Team object

    private void Start()
    {
        // Set up listeners for the buttons and input field
        addScoreButton.onClick.AddListener(OnAddScoreClicked);
        subtractScoreButton.onClick.AddListener(OnSubtractScoreClicked);
        teamNameInputField.onEndEdit.AddListener(OnTeamNameInputEndEdit);
    }

    // Method to assign a Team object and update UI
    public void SetTeam(Team team)
    {
        this.team = team;
        UpdateUI();
    }

    // Method to update the UI to reflect the team's current state
    private void UpdateUI()
    {
        teamNameText.text = team.teamName;  // Update the displayed team name
        teamScoreText.text = "Score: " + team.score;  // Update the displayed score

        // Optionally, reflect the team name in the input field
        teamNameInputField.text = team.teamName;
    }

    // Method called when the Add Score button is clicked
    private void OnAddScoreClicked()
    {
        team.score += 100;  // Increase the score by 100
        UpdateUI();  // Refresh the UI with the updated score
    }

    // Method called when the Subtract Score button is clicked
    private void OnSubtractScoreClicked()
    {
        team.score -= 100;  // Decrease the score by 100
        UpdateUI();  // Refresh the UI with the updated score
    }

    // Method called when the team name input is edited
    private void OnTeamNameInputEndEdit(string newName)
    {
        team.teamName = newName;  // Update the team's name with the new input
        teamNameInputField.text = newName;  // Reflect the new name in the input field
        UpdateUI();  // Refresh the UI to display the updated name
    }
}
