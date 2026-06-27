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

    public GameObject SaveSystem; //should be set to whatever object the 'SaveQuiz' script is attached to
    public GameObject LoadSystem; //should be set to whatever object the 'LoadQuiz' script is attached to

    /// <summary>
    /// Injects scene-object references that cannot live on a prefab.
    /// Must be called while the instance is still disabled (before OnEnable fires).
    /// </summary>
    public void Initialize(GameManager manager, QuestionPanelScreen qScreen, GameObject saveSystem, GameObject loadSystem)
    {
        gameManager = manager;
        questionScreen = qScreen;
        SaveSystem = saveSystem;
        LoadSystem = loadSystem;
    }

    /// <summary>
    /// Re-reads quiz data from the LoadQuiz component and updates the panel text.
    /// Call this after quiz data has been loaded to populate category names,
    /// questions, and answers on panels that were already created.
    /// </summary>
    public void RefreshFromLoadData()
    {
        if (LoadSystem == null)
        {
            Debug.LogWarning("RefreshFromLoadData: LoadSystem is null on panel " + gameObject.name);
            return;
        }

        LoadQuiz loadQuiz = LoadSystem.GetComponent<LoadQuiz>();
        RefreshFromLoadData(loadQuiz);
    }

    /// <summary>
    /// Overload that accepts a pre-resolved LoadQuiz reference, avoiding the
    /// need for each panel to look it up through its own LoadSystem field.
    /// </summary>
    public void RefreshFromLoadData(LoadQuiz loadQuiz)
    {
        if (loadQuiz == null || !loadQuiz.quizLoaded) return;

        if (panelGroup == 0)
        {
            panelText_Primary = loadQuiz.LoadData.Category[panelNumb];
        }
        else
        {
            panelText_Primary = loadQuiz.LoadData.Question[panelNumb];
            panelText_Secondary = loadQuiz.LoadData.Answer[panelNumb];
        }

        // Ensure the panel visual mode is set up (Quiz vs Editor) so that
        // the correct UI elements are visible. This is needed because
        // OnEnable may not have configured the mode successfully.
        if (gameManager != null)
        {
            switch (gameManager.quizPlayMode)
            {
                case GameManager.QuizPlayMode.Quiz:
                    SetPanelContentsToQuiz();
                    break;
                case GameManager.QuizPlayMode.Editor:
                    SetPanelContentsToEditor();
                    break;
            }
        }

        // Update the visible UI text
        if (panelType == PanelType.Category && categoryNameText != null)
        {
            categoryNameText.text = panelText_Primary;
        }
    }

    void OnEnable()
    {
        // Always register button listeners, even if gameManager isn't ready yet
        if (inGameButton != null)
        {
            inGameButton.onClick.AddListener(OpenQuestion); // In-game, question
            inGameButton.onClick.AddListener(CheckIfDailyDouble);
        }

        if (gameManager == null)
        {
            Debug.LogWarning("OnEnable: gameManager is null on panel '" + gameObject.name + "'. "
                + "Initialize() may not have been called yet. Button listeners registered, but skipping mode setup.");

            // Still enable the in-game group so buttons are visible even without gameManager
            if (inGameGroup != null)
                inGameGroup.SetActive(true);

            return;
        }

        //Loads panel data if there is panel data to be loaded. 
        //Shouldn't matter whether it's in the quiz editor or elsewhere, just use the  variables to access relevant data.
        if (LoadSystem != null && LoadSystem.GetComponent<LoadQuiz>().quizLoaded == true)
        {
            if (panelGroup == 0)
            {
                panelText_Primary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Category[panelNumb];
            }
            else
            {
                panelText_Primary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Question[panelNumb];
                panelText_Secondary = LoadSystem.GetComponent<LoadQuiz>().LoadData.Answer[panelNumb];
            }
        }
        else
        {
            //fills with default information. I'll let someone else figure this out.
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
        // If questionScreen wasn't injected (Inspector slot empty), find it in the scene
        if (questionScreen == null)
        {
            questionScreen = FindAnyObjectByType<QuestionPanelScreen>(FindObjectsInactive.Include);
            if (questionScreen != null)
            {
                Debug.LogWarning("OpenQuestion: questionScreen was null on panel '" + gameObject.name
                    + "'. Found one in the scene automatically. "
                    + "Consider assigning it in the OverviewScreen Inspector to avoid this lookup.");
            }
        }

        if (questionScreen == null)
        {
            Debug.LogError("OpenQuestion: No QuestionPanelScreen found anywhere in the scene! "
                + "Make sure a GameObject with the QuestionPanelScreen component exists.");
            return;
        }

        // Pass this panel's data (question + answer loaded from JSON) to the screen
        questionScreen.ShowQuestion(this);
        questionScreen.gameObject.SetActive(true);

        // --- Debug: help diagnose if the screen isn't appearing visually ---
        Debug.Log("OpenQuestion: questionScreen.gameObject.activeSelf = " + questionScreen.gameObject.activeSelf);
        Debug.Log("OpenQuestion: questionScreen.gameObject.activeInHierarchy = " + questionScreen.gameObject.activeInHierarchy);
        Debug.Log("OpenQuestion: questionScreen parent = "
            + (questionScreen.transform.parent != null ? questionScreen.transform.parent.name : "NONE (root)"));
        Debug.Log("OpenQuestion: questionScreen world position = " + questionScreen.transform.position);

        // Check if any parent in the hierarchy is disabled
        Transform current = questionScreen.transform.parent;
        while (current != null)
        {
            if (!current.gameObject.activeSelf)
            {
                Debug.LogWarning("OpenQuestion: PARENT '" + current.name + "' IS DISABLED — the question screen won't be visible!");
            }
            current = current.parent;
        }
    }

    // This function is for exiting the question panel screen, but does not close the question.
    public void ExitQuestion()
    {
        if (questionScreen == null)
            questionScreen = FindAnyObjectByType<QuestionPanelScreen>(FindObjectsInactive.Include);

        if (questionScreen == null)
        {
            Debug.LogWarning("ExitQuestion: questionScreen is null on panel '" + gameObject.name + "'.");
            return;
        }
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
}
