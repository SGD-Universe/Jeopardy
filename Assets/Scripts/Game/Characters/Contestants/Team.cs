using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using Unity.VisualScripting;

public class Team : MonoBehaviour
{
    public string teamName;

    [Range(1, 3)]
    public int teamNumber;

    [SerializeField] private TextMeshProUGUI teamScoreText; // Text used to display the score on the team's podium
    
    public int teamScore;

    [SerializeField] private bool isCurrentPlayer; // This determines if it is the team's turn
    [SerializeField] private bool isIncorrect; /* This determines if a team gets an answer incorrect
    and cannot be called again to answer a question */

    void Awake()
    {
        teamName = "Team " + teamNumber;
        teamScoreText.text = "$" + string.Format(CultureInfo.InvariantCulture, "0:N0", teamScore);
    }

    public void AddPoints(int pointValue)
    {
        teamScore += pointValue;

        UpdateTeamScoreTextColor();
    }

    public void SubtractPoints(int pointValue)
    {
        // pointValue must be positive in order to subtract properly (Real Jeopardy permits negative values)
        teamScore -= pointValue;
        /*
        if (pointValue < 0)
            pointValue = 0;
        */

        UpdateTeamScoreTextColor();
    }

    void UpdateTeamScoreTextColor()
    {
        // If the score is greater than or equal to 0 and the text color is not white...
        if (teamScore >= 0 && teamScoreText.color != Color.white)
        {
            teamScoreText.color = Color.white; // ...change the text color to white
        }

        // If the score is less than 0 and the text color is not red...
        if (teamScore < 0 && teamScoreText.color != Color.red)
        {
            teamScoreText.color = Color.red; // ...change the text color to red
        }
    }
}