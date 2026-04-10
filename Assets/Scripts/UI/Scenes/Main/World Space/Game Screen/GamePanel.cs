using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI categoryNameText;

    [Header("Panel Input Fields")]

    [Header("Panel Properties")]
    [SerializeField] private GameManager.QuizPlayMode quizPlayMode;
    public PanelType panelType;
    public int panelPointValue;

    [Header("Panel Toggles")]
    public bool isDailyDouble;

    [Header("Panel States")]
    public bool isClosed;

    void OnEnable()
    {
        // Have code set for the following combinations:
        // Quiz, category
        // Quiz, question
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

            Debug.Log("AWAKE GAME PANEL: Panel's point value set and formatted!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (quizPlayMode)
        {
            case GameManager.QuizPlayMode.None:
                Debug.LogWarning("AWAKE PLAY MODE WARNING: The play mode of the game is set to None!");
                break;
            case GameManager.QuizPlayMode.Quiz:
                break;
            case GameManager.QuizPlayMode.Editor:
                break;
        }
    }

    public void SetPanelContentsToQuiz()
    {
        if (questionEditorGroup.activeInHierarchy)
        {
            Debug.Log("PANEL CONTENTS - QUIZ: Question Editor group is active in Hierarchy. Disabling object!");

            questionEditorGroup.SetActive(false);
        }

        switch (panelType)
        {
            case PanelType.Category:
                inGameButton.gameObject.SetActive(false);
                categoryNameText.gameObject.SetActive(true);
                Debug.Log("PANEL CONTENTS - QUIZ: Panel type is Category. Showing Category text!");
                break;
            case PanelType.Question:
                categoryNameText.gameObject.SetActive(false);
                inGameButton.gameObject.SetActive(true);
                Debug.Log("PANEL CONTENTS - QUIZ: Panel type is Question: Showing Question Button!");
                break;
        }

        inGameGroup.SetActive(true);

        Debug.Log("PANEL CONTENTS - QUIZ: Panel contents set to Quiz mode!");
    }

    public void SetPanelContentsToEditor()
    {
        if (inGameGroup.activeInHierarchy)
        {
            Debug.Log("PANEL CONTENTS - EDITOR: Quiz group is active in Hierarchy. Disabling object!");

            inGameGroup.SetActive(false);
        }

        switch (panelType)
        {
            case PanelType.Category:
                editQuestionButton.gameObject.SetActive(false);
                editCategoryButton.gameObject.SetActive(true);
                Debug.Log("PANEL CONTENTS - EDITOR: Panel type is Category. Showing Edit Category Button!");
                break;
            case PanelType.Question:
                editCategoryButton.gameObject.SetActive(false);
                editQuestionButton.gameObject.SetActive(true);
                Debug.Log("PANEL CONTENTS - EDITOR: Panel type is Question. Showing Edit Question Button!");
                break;
        }

        questionEditorGroup.SetActive(true);

        Debug.Log("PANEL CONTENTS - EDITOR: Panel contents set to Editor mode!");
    }

    // This function is for opening the question panel screen when a panel is clicked on.
    public void OpenQuestion()
    {

    }

    // This function is for exiting the question panel screen, but does not close the question.
    public void ExitQuestion()
    {

    }

    // This function is for closing a question, meaning that in Quiz mode, the question will no longer be accessed for the remainder of a round.
    public void CloseQuestion()
    {

    }

    public void CheckIfDailyDouble()
    {
        if (isDailyDouble)
        {
            Debug.Log("This panel contains a Daily Double!");
        }
        else
        {
            Debug.Log("This panel does NOT contain a Daily Double!");
        }
    }
}
