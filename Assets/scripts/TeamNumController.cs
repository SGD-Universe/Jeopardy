using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamNumController : MonoBehaviour
{
    public static TeamNumController Instance { get; private set; }
    public Button continueButton; // Button to start the game setup
    public TMP_Text promptText; // Text prompt to guide team selection
    public TMP_Text errorText; // Error message display
    public List<Button> teamButtons; // Buttons for selecting number of teams
    public GameObject teamSelectPanel; // Panel for selecting team number
    public GameObject[] teamPanels; // Panels for individual teams

    private int numberOfTeams; // Tracks the selected number of teams
    public List<Team> Teams { get; private set; } // List of created teams

    private void Awake()
    {
        // Singleton pattern to ensure a single instance
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
        teamSelectPanel.SetActive(true); // Show the team selection panel
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
        Teams = new List<Team>(); // Initialize the team list
        for (int i = 0; i < teamPanels.Length; i++)
        {
            bool isActive = i < numberOfTeams;
            teamPanels[i].SetActive(isActive);
            if (isActive)
            {
                // Initialize team and set up panel for each team
                Team newTeam = new Team("Team " + (i + 1));
                Teams.Add(newTeam);
                TeamPanelController controller = teamPanels[i].GetComponent<TeamPanelController>();
                controller.SetTeam(newTeam); // Set team data in the panel
                Debug.Log("Setting up " + newTeam.teamName);
            }
        }
    }

    public void OnCreateButtonClicked()
    {
        if (numberOfTeams >= 1 && numberOfTeams <= 6)
        {
            Debug.Log("Number of teams: " + numberOfTeams);
            errorText.text = ""; // Clear any previous error messages
            SaveNumberOfTeams();
            SetupTeams();
            teamSelectPanel.SetActive(false); // Hide the team selection panel
            ToCreateGameScene(); // Proceed to the game scene
        }
        else
        {
            errorText.text = "Please select a number between 1 and 6.";
            Debug.Log("Invalid input, number of teams must be between 1 and 6.");
        }
    }

    private void SaveNumberOfTeams()
    {
        PlayerPrefs.SetInt("NumberOfTeams", numberOfTeams);
        PlayerPrefs.Save();
    }

    public void ToCreateGameScene()
    {
        Debug.Log("Transitioning to game screen");
        SceneManager.LoadScene("CreateGame");
    }

    public List<Team> GetTeams()
    {
        return Teams;
    }
}
