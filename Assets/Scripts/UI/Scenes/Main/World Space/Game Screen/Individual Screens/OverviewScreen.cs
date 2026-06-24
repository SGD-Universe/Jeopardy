using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewScreen : MonoBehaviour
{
    const int TOTAL_PANEL_COUNT = 36;
    const int MAXIMUM_QUESTION_PANELS = 30;
    const int MAXIMUM_QUESTION_PANELS_PER_ROW = 6;
    const int MAXIMUM_QUESTION_PANELS_PER_COLUMN = 5;
    const int MAXIMUM_CATEGORY_PANELS = 6;

    // Base point value; each row multiplies this by the row number (1-indexed).
    const int BASE_POINT_VALUE = 200;

    int currentOpenQuestionPanels;
    int remainingOpenQuestionPanels;

    [Header("Game Manager Component")]
    [SerializeField] private GameManager gameManager;

    [Header("Panel Prefab")]
    [SerializeField] private GamePanel gamePanelPrefab;

    [Header("Panel Groups")]
    // These GameObjects should each have a Grid Layout Group component attached
    // in the Unity Editor. The Grid Layout Group handles all positioning/spacing
    // automatically — this script only instantiates panels as children.
    [SerializeField] private GameObject categoryPanelsGroup;
    [SerializeField] private GameObject questionPanelsGroup;

    [Header("Scene References to Inject into Panels")]
    [SerializeField] private QuestionPanelScreen questionScreen;
    [SerializeField] private GameObject saveSystemObject;
    [SerializeField] private GameObject loadSystemObject;

    // Runtime storage for instantiated panels so they can be accessed later
    // (e.g. for loading quiz data, closing questions, etc.).
    private List<GamePanel> categoryPanels = new List<GamePanel>();
    private List<GamePanel> questionPanels = new List<GamePanel>();

    /// <summary>
    /// Re-reads quiz data from the LoadQuiz component and pushes it into every
    /// panel on the board. Call this after LoadQuiz.LoadSavedQuiz() completes
    /// so that category names, questions, and answers are displayed.
    /// </summary>
    public void RefreshAllPanels()
    {
        // Resolve LoadQuiz from the serialized reference, or find it in the scene
        LoadQuiz loadQuiz = null;
        if (loadSystemObject != null)
            loadQuiz = loadSystemObject.GetComponent<LoadQuiz>();
        if (loadQuiz == null)
            loadQuiz = FindAnyObjectByType<LoadQuiz>();

        if (loadQuiz == null || !loadQuiz.quizLoaded)
        {
            Debug.LogWarning("RefreshAllPanels: No loaded quiz data found.");
            return;
        }

        foreach (GamePanel panel in categoryPanels)
        {
            panel.RefreshFromLoadData(loadQuiz);
        }

        foreach (GamePanel panel in questionPanels)
        {
            panel.RefreshFromLoadData(loadQuiz);
        }
    }

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateQuizPanels();

        // If quiz data was already loaded (e.g. user selected a quiz before
        // the game board became active), populate the panels immediately.
        RefreshAllPanels();
    }

    /// <summary>
    /// Creates both the category header row and the question grid beneath it.
    /// Layout is handled entirely by Grid Layout Group components on the parent
    /// GameObjects — this method only instantiates and configures the panels.
    /// </summary>
    void CreateQuizPanels()
    {
        CreateCategoryPanels();
        CreateQuestionPanels();
    }

    /// <summary>
    /// Instantiates one category panel per column into the categoryPanelsGroup.
    /// The Grid Layout Group on categoryPanelsGroup arranges them in a single row.
    /// The prefab is temporarily disabled so OnEnable doesn't fire before
    /// scene references are injected.
    /// </summary>
    void CreateCategoryPanels()
    {
        // Temporarily disable the prefab so Instantiate creates disabled instances
        gamePanelPrefab.gameObject.SetActive(false);

        for (int i = 0; i < MAXIMUM_CATEGORY_PANELS; i++)
        {
            // Instantiate into the Grid Layout Group container (instance starts disabled)
            GamePanel categoryPanelInstance = Instantiate(gamePanelPrefab, categoryPanelsGroup.transform);

            // Configure the *instance*, not the prefab
            categoryPanelInstance.panelType = GamePanel.PanelType.Category;
            categoryPanelInstance.panelGroup = 0;
            categoryPanelInstance.panelNumb = i;

            // Inject scene references before enabling
            categoryPanelInstance.Initialize(gameManager, questionScreen, saveSystemObject, loadSystemObject);

            // Now it's safe to enable — OnEnable will find all references
            categoryPanelInstance.gameObject.SetActive(true);

            categoryPanels.Add(categoryPanelInstance);
        }

        // Restore the prefab to its original active state
        gamePanelPrefab.gameObject.SetActive(true);
    }

    /// <summary>
    /// Instantiates question panels into the questionPanelsGroup in row-major order.
    /// The Grid Layout Group on questionPanelsGroup arranges them into a
    /// MAXIMUM_QUESTION_PANELS_PER_ROW x MAXIMUM_QUESTION_PANELS_PER_COLUMN grid.
    /// Point values escalate per row: $200, $400, $600, $800, $1000.
    /// The prefab is temporarily disabled so OnEnable doesn't fire before
    /// scene references are injected.
    /// </summary>
    void CreateQuestionPanels()
    {
        int panelIndex = 0;

        // Temporarily disable the prefab so Instantiate creates disabled instances
        gamePanelPrefab.gameObject.SetActive(false);

        for (int row = 0; row < MAXIMUM_QUESTION_PANELS_PER_COLUMN; row++)
        {
            // Point value increases with each row (row 0 = $200, row 1 = $400, etc.)
            int rowPointValue = BASE_POINT_VALUE * (row + 1);

            for (int col = 0; col < MAXIMUM_QUESTION_PANELS_PER_ROW; col++)
            {
                // Instantiate into the Grid Layout Group container (instance starts disabled)
                GamePanel questionPanelInstance = Instantiate(gamePanelPrefab, questionPanelsGroup.transform);

                // Configure the *instance*, not the prefab
                questionPanelInstance.panelType = GamePanel.PanelType.Question;
                questionPanelInstance.panelPointValue = rowPointValue;
                questionPanelInstance.panelGroup = 1;
                questionPanelInstance.panelNumb = panelIndex;

                // Inject scene references before enabling
                questionPanelInstance.Initialize(gameManager, questionScreen, saveSystemObject, loadSystemObject);

                // Now it's safe to enable — OnEnable will find all references
                questionPanelInstance.gameObject.SetActive(true);

                questionPanels.Add(questionPanelInstance);
                panelIndex++;
            }
        }

        // Restore the prefab to its original active state
        gamePanelPrefab.gameObject.SetActive(true);
    }
}
