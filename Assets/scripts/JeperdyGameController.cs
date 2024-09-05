using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JeperdyGameController : MonoBehaviour
{
    public static JeperdyGameController Instance { get; private set; }
    public List<Team> Teams { get; private set; }

    public Button playButton;
    public TMP_Text promptText;
    public TMP_Text errorText;
    public List<Button> teamButtons; // List to hold team buttons

    private int numberOfTeams;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           /* DontDestroyOnLoad(gameObject);*/
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //not sure what this does


    private void Start()
    {
        promptText.text = "Select number of teams (1-6):";

        if (playButton == null)
        {
            Debug.LogError("PlayButton is not assigned in the Inspector");
            return;
        }
        //Text for setting teams, and a prompt to remind you to set the teams

        playButton.onClick.AddListener(OnPlayButtonClicked);

        if (errorText != null)
        {
            errorText.text = ""; // Ensure error text is empty at the start
        }

        if (teamButtons == null || teamButtons.Count == 0)
        {
            Debug.LogError("TeamButtons list is not assigned or empty in the Inspector");
            return;
        }
        //sets text and conditions for error notice


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
            //sets team buttons and announces error in setting buttons
        }
    }

    private void OnTeamButtonClicked(int teamCount)
    {
        numberOfTeams = teamCount;
        Debug.Log("Selected number of teams: " + numberOfTeams);
        //sets number of teams based on player selection
    }

  public void OnPlayButtonClicked()
    {
        if (numberOfTeams >= 1 && numberOfTeams <= 6)
        {
            Debug.Log("Number of teams: " + numberOfTeams);
            errorText.text = ""; // Clear any previous error messages
            SetupTeams();
            TransitionToGameScreen();
            //Sets potential amount of teams
        }
        else
        {
            errorText.text = "Please select a number between 1 and 6.";
            Debug.Log("Invalid input, number of teams must be between 1 and 6.");
            //Tells user to select a team amount
        }
    }

    private void SetupTeams()
    {
        Teams = new List<Team>();
        for (int i = 0; i < numberOfTeams; i++)
        {
            Team newTeam = new Team("Team " + (i + 1));
            Teams.Add(newTeam);
            Debug.Log("Setting up " + newTeam.teamName);
        }
        //sets up data for new teams
    }

    public List<Team> GetTeams()
    {
        return Teams;
        //returns team data
    }

    private void TransitionToGameScreen()
    {
        Debug.Log("Transitioning to game screen");
        SceneManager.LoadScene("GameScene");
        //Sets up transition to game scene
    }
}

