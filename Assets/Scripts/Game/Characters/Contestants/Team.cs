using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class Team : MonoBehaviour
{
    [Range(1, 3)]
    public int teamNumber;

    [SerializeField] private TextMeshProUGUI teamScoreText; // The text used to display the score on the team's podium.
    
    public int teamScore;

    [SerializeField] private bool isCurrentPlayer; // This determines if it is the team's turn.

    void Awake()
    {
        teamScoreText.text = "$" + string.Format(CultureInfo.InvariantCulture, "0:N0", teamScore);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddPoints(int pointValue)
    {
        teamScore += pointValue;

        UpdateTeamScoreTextColor();
    }

    public void SubtractPoints(int pointValue)
    {
        // Note: pointValue must be positive in order to subtract properly.

        teamScore -= pointValue;

        UpdateTeamScoreTextColor();
    }

    void UpdateTeamScoreTextColor()
    {
        // If the score is greater than or equal to 0 and the text color is not white...
        if (teamScore >= 0 && teamScoreText.color != Color.white)
        {
            teamScoreText.color = Color.white; // ...change the text color to white.
        }

        // If the score is less than 0 and the text color is not red...
        if (teamScore < 0 && teamScoreText.color != Color.red)
        {
            teamScoreText.color = Color.red; // ...change the text color to red.
        }
    }
}
