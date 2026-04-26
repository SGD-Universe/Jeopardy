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

    [Header("Game Manager Component")]
    [SerializeField] private GameManager gameManager; // This will get the game mode set by the script.

    [Header("Panel Object Groups")]
    [SerializeField] private GameObject questionEditorGroup; // The GameObject that groups all objects related to the Create and Edit Quiz mode.
    [SerializeField] private GameObject inGameGroup; // The GameObject that groups all objects related to the in-game mode.
    [SerializeField] private GameObject panelContentsGroup; // The GameObject that contains all contents of a panel (except the background).

    // By getting the group GameObject, enabling/disabling it will cause it and its children to be enabled/disabled.

    [Header("Screens")]
    [SerializeField] private QuestionPanelScreen questionScreen;

    [Header("Panel Buttons")]
    [SerializeField] private Button editCategoryButton;
    [SerializeField] private Button editQuestionButton;
    [SerializeField] private Button inGameButton; // The button used for showing the point value and question during a game.

    [Header("Panel Text")]
    [SerializeField] private TextMeshProUGUI pointValueText;
    [SerializeField] private TextMeshProUGUI categoryNameText;

    [Header("Panel Input Fields")]
    private int nothing;
    
    [Header("Panel Properties")]
    public PanelType panelType;
    public int panelPointValue = 200;

    [Header("Panel Toggles")]
    public bool isDailyDouble;

    [Header("Panel States")]
    public bool isClosed; // This state should only be used when the quiz play mode is set to Quiz.

    [Header("Panel Save Data")]
    public int panelGroup; //determines whether it is a category or question, only here to prevent moving the 'panelType' variable around
    public int panelNumb; //determines the order in which it belongs in the panels system, which allows the system to know what data to put where
    public string panelText_Primary;
    public string panelText_Secondary; //only for question panels

    [Header("Save/Load Objects")]
    public GameObject SaveSystem; //should be set to whatever object the 'SaveQuiz' script is attached to
    public GameObject LoadSystem; //should be set to whatever object the 'LoadQuiz' script is attached to
    public GameObject GameSave;
    public GameObject GameLoad;

    void OnEnable()
    {
        SaveSystem = GameObject.Find("SaveGameObject");
        LoadSystem = GameObject.Find("LoadGameObject");
        // Have code set for the following combinations:
        // Quiz, category
        // Quiz, question
        // Editor, category
        // Editor, question

        //Loads panel data if there is panel data to be loaded. 
        //Shouldn't matter whether it's in the quiz editor or elsewhere, just use the  variables to access relevant data.
        
        if (GameLoad.GetComponent<LoadGame>().gameLoaded == true)
        {
            if (panelGroup == 0)
            {
                panelText_Primary = GameLoad.GetComponent<LoadGame>().LoadData.Category[panelNumb];
                categoryNameText.text = panelText_Primary;
            }
            else
            {
                panelText_Primary = GameLoad.GetComponent<LoadGame>().LoadData.Question[panelNumb];
                panelText_Secondary = GameLoad.GetComponent<LoadGame>().LoadData.Answer[panelNumb];
                isClosed = GameLoad.GetComponent<LoadGame>().LoadData.Completed[panelNumb];
                isDailyDouble = GameLoad.GetComponent<LoadGame>().LoadData.DDouble[panelNumb];
            }
        }
        if (LoadSystem.GetComponent<LoadQuiz>().quizLoaded == true)
        {
            if (panelGroup == 0)
            {
                panelText_Primary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Category[panelNumb];
                categoryNameText.text = panelText_Primary;
            }
            else
            {
                panelText_Primary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Question[panelNumb];
                panelText_Secondary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Answer[panelNumb];
            }
        }

        // Check the quiz play mode to display the proper elements
        switch (gameManager.quizPlayMode)
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
        SaveSystem = GameObject.Find("SaveQuizObject");
        LoadSystem = GameObject.Find("LoadQuizObject");
        GameSave = GameObject.Find("SaveGameObject");
        GameLoad = GameObject.Find("LoadGameObject");

        switch (gameManager.quizPlayMode)
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
        questionScreen.gameObject.SetActive(true);
    }

    // This function is for exiting the question panel screen, but does not close the question.
    public void ExitQuestion()
    {
        questionScreen.gameObject.SetActive(false);
    }

    // This function is for closing a question, meaning that in Quiz mode, the question will no longer be accessed for the remainder of a round.
    public void CloseQuestion()
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

    //Apply this function after panels contents are edited
    public void SaveContents_Quiz()
    {
        switch (panelType)
        {
            case PanelType.Category:
                SaveSystem.GetComponent<SaveQuiz>().savedQuizCategory[panelNumb] = panelText_Primary;
                break;
            case PanelType.Question:
                SaveSystem.GetComponent<SaveQuiz>().savedQuizQuestion[panelNumb] = panelText_Primary;
                SaveSystem.GetComponent<SaveQuiz>().savedQuizAnswer[panelNumb] = panelText_Secondary;
                break;
        }
    }

    //Apply this function when a 'save game' button is pressed or similar (may require being reassigned elsewhere or given some sort of trigger)
    public void SaveContents_Game()
    {
        switch (panelType)
        {
            case PanelType.Category:
                GameSave.GetComponent<SaveGame>().savedCategory[panelNumb] = panelText_Primary;
                break;
            case PanelType.Question:
                GameSave.GetComponent<SaveGame>().savedQuestion[panelNumb] = panelText_Primary;
                GameSave.GetComponent<SaveGame>().savedAnswer[panelNumb] = panelText_Secondary;
                GameSave.GetComponent<SaveGame>().isComplete[panelNumb] = isClosed;
                //GameSave.GetComponent<SaveGame>().pointValue[panelNumb] =
                GameSave.GetComponent<SaveGame>().isDailyDouble[panelNumb] = isDailyDouble;
                break;
        }
    }
}
