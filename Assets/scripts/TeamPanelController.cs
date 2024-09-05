using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour
{
    public TMP_Text teamNameText;
    public TMP_Text teamScoreText;
    public Button addScoreButton;
    public Button subtractScoreButton;
    public TMP_InputField teamNameInputField; // Reference to the input field

    private Team team;

    private void Start()
    {
        
            addScoreButton.onClick.AddListener(OnAddScoreClicked);

            subtractScoreButton.onClick.AddListener(OnSubtractScoreClicked);


            teamNameInputField.onEndEdit.AddListener(OnTeamNameInputEndEdit);

        // sets up listeners for add and subtact buttons and the team name input
    }

    public void SetTeam(Team team)
    {
        this.team = team;
        UpdateUI();
        //sets teams and calls UpdateUI after
    }

    private void UpdateUI()
    {
        
          teamNameText.text = team.teamName;
        //Sets up team name

       
            teamScoreText.text = "Score: " + team.score;

          /*  teamNameInputField.text = team.teamName; // Display the team name in the input field*/
    }

    private void OnAddScoreClicked()
    {
       
        {
            team.score= team.score + 100;
            UpdateUI();
            //adds score when add button is clicked and updates UI
        }
    }

    private void OnSubtractScoreClicked()
    {
       
        {
            team.score=team.score - 100;
            UpdateUI();
            //subtracts score when add button is clicked and updates UI
        }
    }

    private void OnTeamNameInputEndEdit(string newName)
    {
       
        {
            teamNameInputField.text = newName;
            team.teamName = newName;
            
            UpdateUI();
            //sets up intake and update mechanism for team name
        }
    }
}
