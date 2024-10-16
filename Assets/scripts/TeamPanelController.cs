using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour
{
    //public TMP_Text teamNameText;
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
    }

    public void SetTeam(Team team)
    {
        this.team = team;
        UpdateUI();
    }

    private void UpdateUI()
    {
        
          //teamNameText.text = team.teamName;

       
            teamScoreText.text = "Score: " + team.score;

          /*  teamNameInputField.text = team.teamName; // Display the team name in the input field*/
    }

    private void OnAddScoreClicked()
    {
       
        {
            team.score= team.score + 100;
            UpdateUI();
        }
    }

    private void OnSubtractScoreClicked()
    {
       
        {
            team.score=team.score - 100;
            UpdateUI();
        }
    }

    private void OnTeamNameInputEndEdit(string newName)
    {
       
        {
            teamNameInputField.text = newName;
            
            UpdateUI();
        }
    }
}
