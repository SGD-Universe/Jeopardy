using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    public GameObject saveButtonPrefab;           // Prefab for a button to display each save file
    public Transform contentArea;                 // The parent object where buttons will be placed (inside the Scroll View's Content)
    public TMP_InputField saveFileNameInputField; // Input field to name the save
    public TMP_InputField[] categoryInputFields;  // Array of input fields for category names (e.g., 5 categories)
    public Button saveButton;                     // Save button

    private void Start()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        DisplayAvailableSaves();  // Show available saves as buttons when the UI is initialized
    }

    // Method to handle saving the game state
    private void OnSaveButtonClicked()
    {
        string saveFileName = saveFileNameInputField.text;  // Get the save file name

        // Get category names from the input fields
        string[] categories = new string[categoryInputFields.Length];
        for (int i = 0; i < categoryInputFields.Length; i++)
        {
            categories[i] = categoryInputFields[i].text;  // Save each category name
        }

        if (!string.IsNullOrEmpty(saveFileName))
        {
            // Save the game state with the file name and categories
            GameManager.Instance.SaveUIState(saveFileName, categories);
            DisplayAvailableSaves();  // Refresh the save file display after saving
        }
        else
        {
            Debug.LogError("Save file name cannot be empty!");
        }
    }

    // Method to display available saves as buttons in the Scroll View
    private void DisplayAvailableSaves()
    {
        // Clear existing buttons in the content area (in case they are already there)
        foreach (Transform child in contentArea)
        {
            Destroy(child.gameObject);
        }

        // Get the list of available save files from GameManager
        string[] saveFiles = GameManager.Instance.GetSaveFiles();
        foreach (string filePath in saveFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);  // Get the save file name

            // Instantiate a new button for each save file
            GameObject saveButtonInstance = Instantiate(saveButtonPrefab, contentArea);
            saveButtonInstance.GetComponentInChildren<TMP_Text>().text = fileName;  // Set the button text to the file name

            // Add a listener to load the corresponding save file when the button is clicked
            saveButtonInstance.GetComponent<Button>().onClick.AddListener(() => OnLoadButtonClicked(fileName));
        }
    }

    // Method to load the game state when a save button is clicked
    private void OnLoadButtonClicked(string saveFileName)
    {
        if (!string.IsNullOrEmpty(saveFileName))
        {
            GameManager.Instance.LoadUIState(saveFileName);  // Load the game state using the file name
        }
        else
        {
            Debug.LogError("Save file name cannot be empty!");
        }
    }
}
