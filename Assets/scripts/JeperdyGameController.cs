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
           DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        promptText.text = "Select number of teams (1-6):";

        if (playButton == null)
        {
            Debug.LogError("PlayButton is not assigned in the Inspector");
            return;
        }

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
        Debug.Log("Selected number of teams: " + numberOfTeams);
    }

  public void OnPlayButtonClicked()
    {
        if (numberOfTeams >= 1 && numberOfTeams <= 6)
        {
            Debug.Log("Number of teams: " + numberOfTeams);
            errorText.text = ""; // Clear any previous error messages
            SetupTeams();
            TransitionToGameScreen();
        }
        else
        {
            errorText.text = "Please select a number between 1 and 6.";
            Debug.Log("Invalid input, number of teams must be between 1 and 6.");
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
    }

    public List<Team> GetTeams()
    {
        return Teams;
    }

    private void TransitionToGameScreen()
    {
        Debug.Log("Transitioning to game screen");
        SceneManager.LoadScene("GameScene");
    }
}

