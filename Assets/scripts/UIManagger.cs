using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject loadSaveQuizPanel; // Assign in Inspector
    public Transform saveListContainer;  // Parent object for save buttons
    public GameObject saveButtonPrefab;  // Prefab of a button to represent saves

    private List<string> savedGames = new List<string>(); // Holds saved game names


    public void ShowLoadSaveQuizPanel()
    {
        loadSaveQuizPanel.SetActive(true);
        PopulateSavedGames();
    }

  void PopulateSavedGames()
{
    

    if (saveListContainer == null)
    {
        Debug.LogError("saveListContainer is NOT assigned in the Inspector!");
        return;
    }

    if (saveButtonPrefab == null)
    {
        Debug.LogError("saveButtonPrefab is NOT assigned in the Inspector!");
        return;
    }

    foreach (Transform child in saveListContainer)
    {
        Destroy(child.gameObject);
    }

    savedGames = SaveManager.GetSavedGames();
    if (savedGames == null)
    {
        Debug.LogError("SaveManager.GetSavedGames() returned NULL!");
        savedGames = new List<string>(); // Prevent null reference
    }

    foreach (string saveName in savedGames)
    {
        GameObject newButton = Instantiate(saveButtonPrefab, saveListContainer);
        newButton.GetComponentInChildren<Text>().text = saveName; 
        newButton.GetComponent<Button>().onClick.AddListener(() => LoadGame(saveName));
    }
}

    void LoadGame(string saveName)
    {
        Debug.Log("Loading game: " + saveName);
        SaveManager.LoadGame(saveName); // Call your save manager's LoadGame function
        loadSaveQuizPanel.SetActive(false); // Close the panel after selecting a game
    }
}
