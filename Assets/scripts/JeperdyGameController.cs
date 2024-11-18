using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JeperdyGameController : MonoBehaviour
{
    public static JeperdyGameController Instance { get; private set; }

    public List<TeamPanelController> Teams { get; private set; } // Holds team panel controllers
    public GameObject teamPanelPrefab; // Prefab for the team panels
    public Transform teamPanelParent; // Parent object to hold the team panels

    public TMP_Text errorText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameController alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int numberOfTeams = LoadNumberOfTeams();
        if (numberOfTeams < 1)
        {
            Debug.LogError("No valid number of teams found. Please select a number of teams.");
            errorText.text = "Error: No teams selected!";
            return;
        }

        SetupTeams(numberOfTeams);
    }

    /// <summary>
    /// Loads the selected number of teams from PlayerPrefs.
    /// </summary>
    private int LoadNumberOfTeams()
    {
        return PlayerPrefs.GetInt("NumberOfTeams", 0);
    }

    /// <summary>
    /// Dynamically sets up team panels based on the selected number of teams.
    /// </summary>
    private void SetupTeams(int numberOfTeams)
    {
        Teams = new List<TeamPanelController>();

        if (teamPanelPrefab == null || teamPanelParent == null)
        {
            Debug.LogError("TeamPanelPrefab or TeamPanelParent is not assigned in the Inspector.");
            return;
        }

        for (int i = 0; i < numberOfTeams; i++)
        {
            // Instantiate a new team panel
            GameObject panel = Instantiate(teamPanelPrefab, teamPanelParent);

            // Get the TeamPanelController for the panel
            TeamPanelController controller = panel.GetComponent<TeamPanelController>();
            if (controller != null)
            {
                controller.teamID = (i + 1).ToString(); // Assign unique ID to each team
                controller.teamName = $"Team {i + 1}"; // Default team name
                controller.score = 0; // Initialize score to 0
                controller.UpdateUI(); // Update the panel's UI
                Teams.Add(controller); // Add to the list of teams
            }
            else
            {
                Debug.LogError("TeamPanelController script is not attached to the team panel prefab.");
            }
        }

        Debug.Log($"Successfully set up {Teams.Count} teams.");
    }

    /// <summary>
    /// Returns the list of team panel controllers.
    /// </summary>
    public List<TeamPanelController> GetTeams()
    {
        return Teams;
    }

    /// <summary>
    /// Handles transitioning to the gameplay screen.
    /// </summary>
    public void TransitionToGameScreen()
    {
        Debug.Log("Transitioning to game screen.");
        SceneManager.LoadScene("GameScene");
    }
}
