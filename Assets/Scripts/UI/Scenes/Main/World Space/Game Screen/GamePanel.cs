using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public enum PanelType
    {
        Category,
        Question
    }

    [Header("Panel Object Groups")]
    [SerializeField] private GameObject questionEditorGroup; // The GameObject that groups all objects related to the Create and Edit Quiz mode.
    [SerializeField] private GameObject inGameGroup; // The GameObject that groups all objects related to the in-game mode.

    [Header("Panel Buttons")]
    [SerializeField] private Button editCategoryButton;
    [SerializeField] private Button editQuestionButton;
    [SerializeField] private Button inGameButton; // The button used for showing the point value and question during a game.

    [Header("Panel Properties")]
    public PanelType panelType;
    public int panelPointValue;

    [Header("Panel Toggles")]
    public bool isDailyDouble;

    [Header("Panel States")]
    public bool isClosed;

    void OnEnable()
    {
        // Have code set for the following combinations:
        // In-game, category
        // In-game, question
        // Editor, category
        // Editor, question

        // Check the panel type
        switch (panelType)
        {
            case PanelType.Category:
                
                break;
            case PanelType.Question:

                break;
        }
            

        inGameButton.onClick.AddListener(OpenQuestion); // In-game, question
        inGameButton.onClick.AddListener(CheckIfDailyDouble);
    }

    void OnDisable()
    {
        editCategoryButton.onClick.RemoveAllListeners();
        editQuestionButton.onClick.RemoveAllListeners();

        inGameButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenQuestion()
    {

    }

    public void CloseQuestion()
    {

    }

    public void CheckIfDailyDouble()
    {
        if (isDailyDouble)
        {
            Debug.Log("This panel contains a Daily Double!");
        }
    }
}
