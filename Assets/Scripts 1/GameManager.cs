using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [System.Serializable]
    public class UIState
    {
        public string[] teamNames;   // Store names of the teams
        public int[] teamScores;     // Store scores of the teams
    }

    public UIState currentState;

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveUIState()
    {
        var gameSceneController = FindObjectOfType<GameSceneController>();
        if (gameSceneController != null)
        {
            var teamPanels = gameSceneController.GetComponentsInChildren<TeamPanelController>();
            currentState = new UIState
            {
                teamNames = new string[teamPanels.Length],
                teamScores = new int[teamPanels.Length]
            };

            for (int i = 0; i < teamPanels.Length; i++)
            {
                currentState.teamNames[i] = teamPanels[i].teamNameText.text;
                currentState.teamScores[i] = int.Parse(teamPanels[i].teamScoreText.text.Replace("Score: ", ""));
            }

            PlayerPrefs.SetString("SavedUIState", JsonUtility.ToJson(currentState));
            PlayerPrefs.Save();

            Debug.Log("UI State Saved");
        }
    }

    public void LoadUIState()
    {
        if (PlayerPrefs.HasKey("SavedUIState"))
        {
            string json = PlayerPrefs.GetString("SavedUIState");
            currentState = JsonUtility.FromJson<UIState>(json);

            var gameSceneController = FindObjectOfType<GameSceneController>();
            if (gameSceneController != null)
            {
                var teamPanels = gameSceneController.GetComponentsInChildren<TeamPanelController>();

                for (int i = 0; i < teamPanels.Length; i++)
                {
                    if (i < currentState.teamNames.Length && i < currentState.teamScores.Length)
                    {
                        teamPanels[i].teamNameText.text = currentState.teamNames[i];
                        teamPanels[i].teamScoreText.text = "Score: " + currentState.teamScores[i];
                        teamPanels[i].teamNameInputField.text = currentState.teamNames[i]; // Update input field as well
                    }
                }

                Debug.Log("UI State Loaded");
            }
        }
        else
        {
            Debug.LogError("No saved UI state found!");
        }
    }
}
