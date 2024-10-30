using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour
{
    public string teamID; // Unique identifier for each team
    public string teamName;
    public int score;
    public TMP_Text teamScoreText;
    public Button addScoreButton;
    public Button subtractScoreButton;
    public TMP_InputField teamNameInputField;

    private void Start()
    {
        //LoadData();
        UpdateUI();
        addScoreButton.onClick.AddListener(OnAddScoreClicked);
        subtractScoreButton.onClick.AddListener(OnSubtractScoreClicked);
        teamNameInputField.onEndEdit.AddListener(OnTeamNameInputEndEdit);
    }

    // Method to display the current score and team name
    private void UpdateUI()
    {
        teamScoreText.text = "Score: " + score;
        teamNameInputField.text = teamName;
    }

    //Method to add to the score
    private void OnAddScoreClicked()
    {
        score += 100;
        UpdateUI();
        SaveData();
    }

    //Method to subtract from the score
    private void OnSubtractScoreClicked()
    {
        score -= 100;
        UpdateUI();
        SaveData();
    }

    //Method to input team name
    private void OnTeamNameInputEndEdit(string newName)
    {
        teamName = newName;
        SaveData();
        UpdateUI();
    }

    //Method to save score and team name
    private void SaveData()
    {
        PlayerPrefs.SetInt("TeamScore_" + teamID, score);
        PlayerPrefs.SetString("TeamName_" + teamID, teamName);
        PlayerPrefs.Save();
    }

    /*public void LoadData()
    {
        score = PlayerPrefs.GetInt("TeamScore_" + teamID, 0);
        teamName = PlayerPrefs.GetString("TeamName_" + teamID, teamName);
    }*/
}
