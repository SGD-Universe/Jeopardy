using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public GameObject teamPanelPrefab;
    public Transform teamPanelParent; // This should be the TeamPanelContainer

    private List<TeamPanelController> teamPanelControllers;

    private void Start()
    {
        Debug.Log("GameSceneController Start");
        teamPanelControllers = new List<TeamPanelController>();
        CreateTeamPanels();
    }

    private void CreateTeamPanels()
    {
        Debug.Log("CreateTeamPanels called");
        var teams = JeperdyGameController.Instance.GetTeams();
        Debug.Log("Number of teams: " + teams.Count);

        for (int i = 0; i < teams.Count; i++)
        {
            Debug.Log("Creating panel for team: " + teams[i].teamName);
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