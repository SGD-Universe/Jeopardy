using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour
{
    // Team data
    public string teamName;
    public int score;

    // UI elements
    //public TMP_Text teamNameText;
    public TMP_Text teamScoreText;
    public Button addScoreButton;
    public Button subtractScoreButton;
    public TMP_InputField teamNameInputField; // Reference to the input field

    private void Start()
    {
        // Set up button listeners
        addScoreButton.onClick.AddListener(OnAddScoreClicked);
        subtractScoreButton.onClick.AddListener(OnSubtractScoreClicked);
        teamNameInputField.onEndEdit.AddListener(OnTeamNameInputEndEdit);

        // Initialize UI
        UpdateUI();
    }

    private void UpdateUI()
    {
        //teamNameText.text = teamName;
        teamScoreText.text = "Score: " + score;
        teamNameInputField.text = teamName; // Display the team name in the input field
    }

    private void OnAddScoreClicked()
    {
        score += 100;
        UpdateUI();
    }

    private void OnSubtractScoreClicked()
    {
        if (score > 0)
        {
            score -= 100;
            UpdateUI();
        }
    }

    private void OnTeamNameInputEndEdit(string newName)
    {
        teamName = newName;
        UpdateUI();
    }
}
