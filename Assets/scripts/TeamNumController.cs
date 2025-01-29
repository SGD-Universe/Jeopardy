using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamNumController : MonoBehaviour
{
    public static TeamNumController Instance { get; private set; }
    public Button continueButton;
    public TMP_Text promptText;
    public TMP_Text errorText;
    public List<Button> teamButtons;
    private int numberOfTeams;
    public GameObject teamSelectPanel;
    public GameObject[] teamPanels;

    private void Start()
    {
        // Unused but functional load function
        /*if (LoadNumberOfTeams())
        {
            SetupTeams();
            teamSelectPanel.SetActive(false);
        }
        else
        {
            teamSelectPanel.SetActive(true);
            promptText.text = "Please select a number of teams:";
        }*/

        teamSelectPanel.SetActive(true);
        promptText.text = "Please select a number of teams:";

        if (continueButton == null)
        {
            Debug.LogError("continueButton is not assigned in the Inspector");
            return;
        }

        continueButton.onClick.AddListener(OnCreateButtonClicked);

        if (errorText != null)
        {
            errorText.text = ""; // Ensure error text is empty at the start
        }

        if (teamButtons == null || teamButtons.Count == 0)
        {
            Debug.LogError("TeamButtons list is not assigned or empty in the Inspector");
            return;
        }

        // Add listeners to the team buttons
        for (int i = 0; i < teamButtons.Count; i++)
        {
            if (teamButtons[i] != null)
            {
                int index = i; // Local copy of the loop variable // Why????
                teamButtons[i].onClick.AddListener(() => OnTeamButtonClicked(index + 1));
            }
            else
            {
                Debug.LogError("A TeamButton is not assigned in the Inspector at index: " + i);
            }
        }
    }

    private void OnTeamButtonClicked(int teamCount)
    {
        numberOfTeams = teamCount;
        promptText.text = "Selected number of teams: " + numberOfTeams;
    }

    public void SetupTeams()
    {
        for (int i = 0; i < teamPanels.Length; i++)
        {
            teamPanels[i].SetActive(i < numberOfTeams);
            /*if (i < numberOfTeams)
            {
                TeamPanelController controller = teamPanels[i].GetComponent<TeamPanelController>();
                controller.LoadData();
            }*/
        }
    }

    public void OnCreateButtonClicked()
    {
        if (numberOfTeams >= 1 && numberOfTeams <= 6)
        {
            Debug.Log("Number of teams: " + numberOfTeams);
            errorText.text = ""; // Clear any previous error messages
            SaveNumberOfTeams();
            SetupTeams();
            teamSelectPanel.gameObject.SetActive(false);
        }
        else
        {
            errorText.text = "Please select a number between 1 and 4.";
            Debug.Log("Invalid input, number of teams must be between 1 and 4.");
        }
    }

    private void SaveNumberOfTeams()
    {
        PlayerPrefs.SetInt("NumberOfTeams", numberOfTeams);
        PlayerPrefs.Save();
    }

    // Unused but functional load function
    /*private bool LoadNumberOfTeams()
    {
        numberOfTeams = PlayerPrefs.GetInt("NumberOfTeams", 0);
        return numberOfTeams > 0;
    }*/
}
