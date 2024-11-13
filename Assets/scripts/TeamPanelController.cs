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

    // Arrays for each point panel type
    public GameObject[] twoHundredPointPanels;
    public GameObject[] threeHundredPointPanels;
    public GameObject[] fourHundredPointPanels;
    public GameObject[] fiveHundredPointPanels;

    private void Start()
    {
        UpdateUI();
        addScoreButton.onClick.AddListener(OnAddScoreClicked);
        subtractScoreButton.onClick.AddListener(OnSubtractScoreClicked);
        teamNameInputField.onEndEdit.AddListener(OnTeamNameInputEndEdit);
    }

    private void UpdateUI()
    {
        teamScoreText.text = "Score: " + score;
        teamNameInputField.text = teamName;
    }

    private void OnAddScoreClicked()
    {
        if (IsPanelActive(twoHundredPointPanels))
        {
            score += 200;
        }
        else if (IsPanelActive(threeHundredPointPanels))
        {
            score += 300;
        }
        else if (IsPanelActive(fourHundredPointPanels))
        {
            score += 400;
        }
        else if (IsPanelActive(fiveHundredPointPanels))
        {
            score += 500;
        }
        else
        {
            score += 100;
        }

        UpdateUI();
        SaveData();
    }

    private void OnSubtractScoreClicked()
    {
        if (IsPanelActive(twoHundredPointPanels))
        {
            score -= 200;
        }
        else if (IsPanelActive(threeHundredPointPanels))
        {
            score -= 300;
        }
        else if (IsPanelActive(fourHundredPointPanels))
        {
            score -= 400;
        }
        else if (IsPanelActive(fiveHundredPointPanels))
        {   
            score -= 500;
        }
        else
        {
            score -= 100;
        }

        UpdateUI();
        SaveData();
    }

    private void OnTeamNameInputEndEdit(string newName)
    {
        teamName = newName;
        SaveData();
        UpdateUI();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("TeamScore_" + teamID, score);
        PlayerPrefs.SetString("TeamName_" + teamID, teamName);
        PlayerPrefs.Save();
    }

    // Method to check if any panel in the given array is active
    private bool IsPanelActive(GameObject[] panels)
    {
        foreach (GameObject panel in panels)
        {
            if (panel.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    /*public void LoadData()
    {
        score = PlayerPrefs.GetInt("TeamScore_" + teamID, 0);
        teamName = PlayerPrefs.GetString("TeamName_" + teamID, teamName);
    }*/
}
