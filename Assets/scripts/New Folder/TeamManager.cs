using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour
{
    public GameObject teamPrefab;
    public List<Team> teams = new List<Team>();

    public void AddTeam()
    {
        // Instantiate a new team object
        GameObject newTeamObj = Instantiate(teamPrefab);
        Team newTeam = newTeamObj.GetComponent<Team>();
        newTeam.Initialize("Team " + (teams.Count + 1)); // You can customize the name as needed
        teams.Add(newTeam);
    }

    public void RemoveTeam(Team team)
    {
        teams.Remove(team);
        Destroy(team.gameObject);
    }
}