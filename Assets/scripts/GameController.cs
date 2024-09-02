using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public GameObject teamPanelPrefab;
    public Transform teamPanelParent; // This should be the TeamPanelContainer

    public List<TeamPanelController> teamPanelControllers;

    private void Start()
    {
        Debug.Log("GameSceneController Start");
        teamPanelControllers = new List<TeamPanelController>();

        if (JeperdyGameController.Instance == null)
        {
            Debug.LogError("JeperdyGameController instance is not set. Make sure it's initialized in the previous scene.");
            return;
        }

        CreateTeamPanels();
    }

    private void CreateTeamPanels()
    {
        Debug.Log("CreateTeamPanels called");

        var teams = JeperdyGameController.Instance.GetTeams();
        if (teams == null || teams.Count == 0)
        {
            Debug.LogError("No teams found. Ensure SetupTeams() was called before transitioning to the game scene.");
            return;
        }

        Debug.Log("Number of teams: " + teams.Count);

        for (int i = 0; i < teams.Count; i++)
        {
            Debug.Log("Creating panel for team: " + teams[i].teamName);

            if (teamPanelPrefab == null)
            {
                Debug.LogError("teamPanelPrefab is not set in the inspector!");
                continue;
            }
            if (teamPanelParent == null)
            {
                Debug.LogError("teamPanelParent is not set in the inspector!");
                continue;
            }

            GameObject panel = Instantiate(teamPanelPrefab, teamPanelParent);
            if (panel == null)
            {
                Debug.LogError("Failed to instantiate team panel prefab");
                continue;
            }

            TeamPanelController controller = panel.GetComponent<TeamPanelController>();
            if (controller == null)
            {
                Debug.LogError("TeamPanelController component missing on instantiated prefab");
                continue;
            }

            controller.SetTeam(teams[i]);
            teamPanelControllers.Add(controller);
        }
    }
}
