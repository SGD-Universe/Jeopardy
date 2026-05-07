using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class GamePanel : MonoBehaviour
{
    // The enumerator that lists the different panel types. Each item is tied to a constant (in this case, Category is equal to 0, and Question is equal to 1).
    public enum PanelType
    {
        Category,
        Question
    }

    private GameManager gameManager; // This will get the game mode set by the script.

    [Header("Panel Object Groups")]
    [SerializeField] private GameObject questionEditorGroup; // The GameObject that groups all objects related to the Create and Edit Quiz mode.
    [SerializeField] private GameObject inGameGroup; // The GameObject that groups all objects related to the in-game mode.
    [SerializeField] private GameObject panelContentsGroup; // The GameObject that contains all contents of a panel (except the background).

    // By getting the group GameObject, enabling/disabling it will cause it and its children to be enabled/disabled.

    [Header("Screens")]
    [SerializeField] private QuestionPanelScreen questionScreen; // This (at least the object with this component attached) needs to be a prefab.

    [Header("Panel Buttons")]
    [SerializeField] private Button editCategoryButton;
    [SerializeField] private Button editQuestionButton;
    [SerializeField] private Button inGameButton; // The button used for showing the point value and question during a game.

    [Header("Panel Text")]
    public TextMeshProUGUI pointValueText;
    [SerializeField] private TextMeshProUGUI categoryNameText;

    [Header("Panel Input Fields")]
    private int nothing;
    
    [Header("Panel Properties")]
    public PanelType panelType;
    public int panelPointValue = 200; // The base point value of each question panel. Will be multiplied based on the row the panel is on.
    public int panelXCoordinate; // Coordinate system used for keeping track of a panel's placement on the category and question layouts.
    public int panelYCoordinate;

    [Header("Panel Toggles")]
    public bool isDailyDouble;

    [Header("Panel States")]
    public bool isClosed; // This state should only be used when the game's play mode is set to Quiz.

    //[Header("Panel Save Data")]
    //public int panelGroup; //determines whether it is a category or question, only here to prevent moving the 'panelType' variable around
    //public int panelNumb; //determines the order in which it belongs in the panels system, which allows the system to know what data to put where
    //public string panelText_Primary;
    //public string panelText_Secondary; //only for question panels

    //public GameObject SaveSystem; //should be set to whatever object the 'SaveQuiz' script is attached to
    //public GameObject LoadSystem; //should be set to whatever object the 'LoadQuiz' script is attached to

    void Awake()
    {
        if (panelType == PanelType.Question)
        {
            FormatPointsText(pointValueText);

            //Debug.Log("AWAKE - GAME PANEL - PANEL TYPE - QUESTION: Panel's point value set and formatted!");
        }
    }

    void OnEnable()
    {
        // Have code set for the following combinations:
        // Quiz, category
        // Quiz, question
        // Editor, category
        // Editor, question

        //Loads panel data if there is panel data to be loaded. 
        //Shouldn't matter whether it's in the quiz editor or elsewhere, just use the  variables to access relevant data.
        //if (LoadSystem.GetComponent<LoadQuiz>().quizLoaded == true)
        //{
            //if (panelGroup == 0)
            //{
                //panelText_Primary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Category[panelNumb];
            //}
            //else
            //{
                //panelText_Primary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Question[panelNumb];
                //panelText_Secondary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Answer[panelNumb];
            //}
        //}
        //else
        //{
            //fills with default information. I'll let someone else figure this out.
        //}

        inGameButton.onClick.AddListener(OpenQuestion); // In-game, question
        //Debug.Log("ON ENABLE - GAME PANEL: Added the OpenQuestion function to In-Game Button's OnClick event!");
        inGameButton.onClick.AddListener(CheckIfDailyDouble);
        //Debug.Log("ON ENABLE - GAME PANEL: Added the CheckIfDailyDouble function to In-Game Button's OnClick event!");
    }

    void OnDisable()
    {
        //Debug.Log("ON DISABLE - GAME PANEL: OnDisable function called!");

        editCategoryButton.onClick.RemoveAllListeners();
        //Debug.Log("ON DISABLE - GAME PANEL: Removed all functions from Edit Category Button's OnClick event!");
        editQuestionButton.onClick.RemoveAllListeners();
        //Debug.Log("ON DISABLE - GAME PANEL: Removed all functions from Edit Question Button's OnClick event!");

        inGameButton.onClick.RemoveAllListeners();
        //Debug.Log("ON DISABLE - GAME PANEL: Removed all functions from In-Game Button's OnClick event!");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        //Debug.Log("START - GAME PANEL: Start function called!");

        // If the Game Manager's game mode is not set to None...
        if (gameManager.quizPlayMode != GameManager.QuizPlayMode.None)
        {
            SetUpPanelContents(); // ...set up the panel's contents.
        }
        else
        {
            Debug.LogWarning("START - GAME PANEL - GAME MODE - NONE: The Game Manager's game mode is set to None!");
        }
    }

    public void SetUpPanelContents()
    {
        //Debug.Log("SET UP PANEL CONTENTS: Setting up panel's contents!");

        if (gameManager.quizPlayMode == GameManager.QuizPlayMode.Quiz)
        {
            //Debug.Log("SET UP PANEL CONTENTS - QUIZ: Game mode is Quiz!");

            // If the quiz editor group is active in the Hierarchy window...
            if (questionEditorGroup.activeInHierarchy)
            {
                //Debug.Log("SET UP PANEL CONTENTS - QUIZ: Question Editor group is active in Hierarchy. Disabling object!");

                questionEditorGroup.SetActive(false); // ...set its active state to false.
            }

            // Check the panel's type
            switch (panelType)
            {
                case PanelType.Category:
                    ShowQuizCategory();
                    break;
                case PanelType.Question:
                    ShowQuizQuestion();
                    break;
            }

            inGameGroup.SetActive(true);

            //Debug.Log("SET UP PANEL CONTENTS - QUIZ: Panel contents set to Quiz mode!");
        }
        else if (gameManager.quizPlayMode == GameManager.QuizPlayMode.Editor)
        {
            //Debug.Log("SET UP PANEL CONTENTS - EDITOR: Game mode is Editor!");

            // If the in-game group is active in the Hierarchy window...
            if (inGameGroup.activeInHierarchy)
            {
                //Debug.Log("SET UP PANEL CONTENTS - EDITOR: Quiz group is active in Hierarchy. Disabling object!");

                inGameGroup.SetActive(false); // ...set its active state to false.
            }

            // Check the panel's type
            switch (panelType)
            {
                case PanelType.Category:
                    ShowEditorCategory();
                    break;
                case PanelType.Question:
                    ShowEditorQuestion();
                    break;
            }

            questionEditorGroup.SetActive(true);

            //Debug.Log("SET UP PANEL CONTENTS - EDITOR: Panel contents set to Editor mode!");
        }
    }

    void ShowQuizCategory()
    {
        panelPointValue = 0; // Category panels do not need points. Added in case a bug somehow adds points to a team's score.

        inGameButton.gameObject.SetActive(false);
        categoryNameText.gameObject.SetActive(true);
        Debug.Log("SHOW QUIZ CATEGORY: Panel type is Category. Showing Category text!");
    }

    void ShowQuizQuestion()
    {
        categoryNameText.gameObject.SetActive(false);
        inGameButton.gameObject.SetActive(true);
        Debug.Log("SHOW QUIZ QUESTION: Panel type is Question: Showing Question Button!");
    }

    void ShowEditorCategory()
    {
        panelPointValue = 0;

        editQuestionButton.gameObject.SetActive(false);
        editCategoryButton.gameObject.SetActive(true);
        Debug.Log("SHOW EDITOR CATEGORY: Panel type is Category. Showing Edit Category Button!");
    }

    void ShowEditorQuestion()
    {
        editCategoryButton.gameObject.SetActive(false);
        editQuestionButton.gameObject.SetActive(true);
        Debug.Log("SHOW EDITOR QUESTION: Panel type is Question. Showing Edit Question Button!");
    }

    // This function is for opening the question panel screen when a panel is clicked on.
    void OpenQuestion()
    {
        questionScreen.gameObject.SetActive(true);
    }

    // This function is for exiting the question panel screen, but does not close the question.
    void ExitQuestion()
    {
        questionScreen.gameObject.SetActive(false);
    }

    // This function is for closing a question, meaning that in Quiz mode, the question will no longer be accessed for the remainder of a round.
    void CloseQuestion()
    {
        if (gameManager.quizPlayMode == GameManager.QuizPlayMode.Quiz)
        {
            HideGamePanelContents();

            isClosed = true;
        }
    }

    public void CheckIfDailyDouble()
    {
        switch (isDailyDouble)
        {
            case true:
                Debug.Log("This panel contains a Daily Double!");
                break;
            case false:
                Debug.Log("This panel does NOT contain a Daily Double!");
                break;
        }
    }

    public void AddPoints(int points, Team targetTeam)
    {
        targetTeam.teamScore += points;
    }

    public void SubtractPoints(int points, Team targetTeam)
    {
        targetTeam.teamScore -= points;
    }

    // This function will disable the panel's contents.
    void HideGamePanelContents()
    {
        panelContentsGroup.SetActive(false);
    }

    public void FormatPointsText(TextMeshProUGUI pointsText)
    {
        pointsText.text = "$" + string.Format(CultureInfo.InvariantCulture, "{0:N0}", panelPointValue); // This will format the text with comma separators.
    }
}
