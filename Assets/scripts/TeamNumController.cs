
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamNumController : MonoBehaviour
{
    public static TeamNumController Instance { get; private set; }
    public List<Team> Teams { get; private set; }

    public Button continueButton;
    public TMP_Text promptText;
    public TMP_Text errorText;
    public List<Button> teamButtons;

    private int numberOfTeams;

    public GameObject teamSelectPanel;


    private void Start()
    {
       teamSelectPanel.SetActive(true);
       promptText.text = "Please select a number of teams:";

        if (continueButton == null)
        {
            Debug.LogError("continueButton is not assigned in the Inspector");
            return;
        }

        continueButton.onClick.AddListener(OnCreateButtonClicked);

        if (errorText != null)
        {
            errorText.text = ""; // Ensure error text is empty at the start
        }

        if (teamButtons == null || teamButtons.Count == 0)
        {
            Debug.LogError("TeamButtons list is not assigned or empty in the Inspector");
            return;
        }

        // Add listeners to the team buttons
        for (int i = 0; i < teamButtons.Count; i++)
        {
            if (teamButtons[i] != null)
            {
                int index = i; // Local copy of the loop variable
                teamButtons[i].onClick.AddListener(() => OnTeamButtonClicked(index + 1));
            }
            else
            {
                Debug.LogError("A TeamButton is not assigned in the Inspector at index: " + i);
            }
        }
    }

    private void OnTeamButtonClicked(int teamCount)
    {
        numberOfTeams = teamCount;
        promptText.text = "Selected number of teams: " + numberOfTeams;
    }

    public void SetupTeams()
    {
        Teams = new List<Team>();
        for (int i = 0; i < numberOfTeams; i++)
        {
            Team newTeam = new Team("Team " + (i + 1));
            Teams.Add(newTeam);
            Debug.Log("Setting up " + newTeam.teamName);
        }
    }



    public void OnCreateButtonClicked()
    {
        if (numberOfTeams >= 1 && numberOfTeams <= 6)
        {
            Debug.Log("Number of teams: " + numberOfTeams);
            errorText.text = ""; // Clear any previous error messages
            SetupTeams();
            teamSelectPanel.gameObject.SetActive(false);
        }
        else
        {
            errorText.text = "Please select a number between 1 and 6.";
            Debug.Log("Invalid input, number of teams must be between 1 and 6.");
        }
    }

    public List<Team> GetTeams()
    {
        return Teams;
    }

}

