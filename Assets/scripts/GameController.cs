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
        //Creates new list for team panel controllers
        CreateTeamPanels();
        //Identifies console prompt and calls for creation of team panels
    }

    private void CreateTeamPanels()
    {
        Debug.Log("CreateTeamPanels called");
        var teams = JeperdyGameController.Instance.GetTeams();
        Debug.Log("Number of teams: " + teams.Count);
        //identifies and calls on prompt for number of teams

        for (int i = 0; i < teams.Count; i++)
        {
            Debug.Log("Creating panel for team: " + teams[i].teamName);
            GameObject panel = Instantiate(teamPanelPrefab, teamPanelParent);
            if (panel == null)
            {
                Debug.LogError("Failed to instantiate team panel prefab");
                continue;
                //accounts for failure to create team panel prefab
            }

            TeamPanelController controller = panel.GetComponent<TeamPanelController>();
            if (controller == null)
            {
                Debug.LogError("TeamPanelController component missing on instantiated prefab");
                continue;
                //accounts for failure to find team panel prefab
            }

            controller.SetTeam(teams[i]);
            //sets amount of teams
            teamPanelControllers.Add(controller);
            //adds controller for the team panels

        }
    }
}