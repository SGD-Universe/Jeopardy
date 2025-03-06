using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameUI : MonoBehaviour
{
    public SaveManager saveManager;
    public GameObject loadGamePanel;
    public Transform savedGamesContent;
    public GameObject loadGameButtonPrefab;

    void Start()
    {
        if (loadGamePanel != null)
        {
            loadGamePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("LoadGamePanel is NOT assigned in the Inspector!");
        }
    }

    public void ShowLoadGamePanel()
    {
        if (loadGamePanel != null)
        {
            loadGamePanel.SetActive(true);
            PopulateSaveList();
        }
    }

    public void HideLoadGamePanel()
    {
        if (loadGamePanel != null)
        {
            loadGamePanel.SetActive(false);
        }
    }

    void PopulateSaveList()
    {
        if (savedGamesContent == null || loadGameButtonPrefab == null)
        {
            Debug.LogError("SavedGamesContent or LoadGameButtonPrefab is NOT assigned in the Inspector!");
            return;
        }

        // Clear existing buttons
        foreach (Transform child in savedGamesContent)
        {
            Destroy(child.gameObject);
        }

        // Fetch saved game names
        List<string> saveFiles = SaveManager.GetSavedGames();
        if (saveFiles.Count == 0)
        {
            Debug.LogWarning("No saved games found!");
            return;
        }

        foreach (string save in saveFiles)
        {
            GameObject button = Instantiate(loadGameButtonPrefab, savedGamesContent);
            Text buttonText = button.GetComponentInChildren<Text>();
            
            if (buttonText != null)
            {
                buttonText.text = save;
            }

            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.RemoveAllListeners(); // Prevent duplicate listeners
                buttonComponent.onClick.AddListener(() => LoadSelectedGame(save));
            }
        }
    }

    void LoadSelectedGame(string saveName)
    {
        Debug.Log("Loading game: " + saveName);
        SaveManager.LoadGame(saveName); // You might need to modify this if `LoadGame` requires a name
        HideLoadGamePanel();
    }
}
