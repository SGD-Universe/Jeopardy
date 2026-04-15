using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

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

    // By getting the group GameObject, enabling/disabling it will cause it and its children to be enabled/disabled.

    [Header("Panel Buttons")]
    [SerializeField] private Button editCategoryButton;
    [SerializeField] private Button editQuestionButton;
    [SerializeField] private Button inGameButton; // The button used for showing the point value and question during a game.

    [Header("Panel Text")]
    [SerializeField] private TextMeshProUGUI pointValueText;

    [Header("Panel Input Fields")]

    [Header("Panel Properties")]
    [SerializeField] private GameManager.QuizPlayMode quizPlayMode;
    public PanelType panelType;
    public int panelPointValue;

    [Header("Panel Toggles")]
    public bool isDailyDouble;

    [Header("Panel States")]
    public bool isClosed;

    [Header("Panel Save Data - Placeholders")]
    public int panelGroup; //determines whether it is a category or question, only here to prevent moving the 'panelType' variable around
    public int panelNumb; //determines the order in which it belongs in the panels system, which allows the system to know what data to put where
    public string panelText_Primary;
    public string panelText_Secondary;

    public bool loadedQuiz;

    void OnEnable()
    {
        // Have code set for the following combinations:
        // In-game, category
        // In-game, question
        // Editor, category
        // Editor, question

        // Check the quiz play mode to display the proper elements
        switch (quizPlayMode)
        {
            case GameManager.QuizPlayMode.None:

                break;
            case GameManager.QuizPlayMode.Quiz:
                SetPanelContentsToQuiz();
                
                break;
            case GameManager.QuizPlayMode.Editor:
                SetPanelContentsToEditor();
                
                break;
        }

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

    void Awake()
    {
        if (panelType == PanelType.Question)
        {
            pointValueText.text = "$" + string.Format(CultureInfo.InvariantCulture, "{0:N0}", panelPointValue); // This will format the text with comma separators.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPanelContentsToQuiz()
    {
        if (questionEditorGroup.activeInHierarchy)
        {
            questionEditorGroup.SetActive(false);
        }

        inGameGroup.SetActive(true);
    }

    public void SetPanelContentsToEditor()
    {

        if (inGameGroup.activeInHierarchy)
        {
            inGameGroup.SetActive(false);
        }

        switch (panelType)
        {
            case PanelType.Category:
                editCategoryButton.gameObject.SetActive(true);
                break;
            case PanelType.Question:
                editQuestionButton.gameObject.SetActive(true);
                break;
        }

        if (loadedQuiz == true)
        {
            //fill with data from an array that contains the information regarding loaded save files
        }
        else
        {
            //fills with default information, preferably the 'saveQuiz' string information from "Placeholder"
        }

        questionEditorGroup.SetActive(true);
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
