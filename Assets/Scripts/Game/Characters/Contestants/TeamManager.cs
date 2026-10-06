using UnityEngine;
/*
Manages exactly two teams for a Jeopardy game.

This is a singleton — only one TeamManager should exist in the scene.
It holds references to Team 1 and Team 2 (which each use the existing Team script)
and provides helper methods to look up a team by number, get scores, and
reset scores between rounds.

========== WHERE TO ATTACH IN UNITY ==========
1. Create an empty GameObject in your scene hierarchy.
   - Right-click in the Hierarchy → Create Empty.
   - Rename it to "TeamManager".
2. Drag this script (TeamManager.cs) onto the "TeamManager" GameObject
   in the Inspector.
3. In the Inspector, you will see two fields:
      • Team One  — drag the GameObject that has the Team script with teamNumber = 1.
      • Team Two  — drag the GameObject that has the Team script with teamNumber = 2.
   These are the existing Team GameObjects in your scene (e.g. the podium objects
   that already have Team.cs attached).
==============================================
*/
public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance; // Singleton

    [Header("Team References")] // Assign in Inspector
    [Tooltip("Drag the GameObject that has the Team script with teamNumber = 1.")]
    [SerializeField] private Team teamOne;

    [Tooltip("Drag the GameObject that has the Team script with teamNumber = 2.")]
    [SerializeField] private Team teamTwo;

    // Team 3?

    // Public read-only accessors
    /// <summary> Returns the Team 1 reference </summary>
    public Team TeamOne => teamOne;
    /// <summary> Returns the Team 2 reference </summary>
    public Team TeamTwo => teamTwo;

    // Unity Lifecycle
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // This maintains the gameObject should the scene change
    }

#region Helper Methods
    // Returns the Team whose teamNumber matches the given number (1 or 2)
    /// <summary> Returns null if the number is invalid or the reference is unassigned </summary>
    public Team GetTeamByNumber(int teamNumber)
    {
        switch (teamNumber)
        {
            case 1: return teamOne;
            case 2: return teamTwo;
            default:
                Debug.LogWarning("TeamManager: Invalid team number " + teamNumber + ". Expected 1 or 2.");
                return null;
        }
    }

    // Returns the score of the specified team (1 or 2)
    /// <summary> Returns 0 if the team number is invalid </summary>
    public int GetTeamScore(int teamNumber)
    {
        Team team = GetTeamByNumber(teamNumber);
        return team != null ? team.teamScore : 0;
    }

    /* Resets both teams' scores to zero
    Useful when starting a new round or a new game */
    public void ResetAllScores()
    {
        if (teamOne != null)
        {
            teamOne.teamScore = 0;
            // Trigger the score text update by adding 0 points
            teamOne.AddPoints(0);
        }

        if (teamTwo != null)
        {
            teamTwo.teamScore = 0;
            teamTwo.AddPoints(0);
        }
    }
#endregion Helper Methods
}