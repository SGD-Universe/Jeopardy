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

    int currentOpenQuestionPanels;
    int remainingOpenQuestionPanels;

    private GameManager gameManager;

    [Header("Panel Prefab")]
    [SerializeField] private GamePanel gamePanelPrefab;

    [Header("Panel Groups")]
    [SerializeField] private GameObject categoryPanelsGroup;
    [SerializeField] private GameObject questionPanelsGroup;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        // If the Game Manager's game mode is not set to None...
        if (gameManager.quizPlayMode != GameManager.QuizPlayMode.None)
        {
            CreateQuizPanels(gamePanelPrefab); // ...create the panels.
        }
        else
        {
            Debug.LogWarning("START - OVERVIEW SCREEN: The Game Manager's game mode is set to None!");
        }
    }

    void CreateQuizPanels(GamePanel panelPrefab)
    {
        CreateCategoryPanels(panelPrefab);
        CreateQuestionPanels(panelPrefab);
    }

    // TODO: Implement the instantiation of game panels in a grid using the Grid Layout Group components attached to the Category Panels and Quiz Panels GameObjects.

    void CreateCategoryPanels(GamePanel categoryPanel)
    {
        for (int x = 0; x < MAXIMUM_CATEGORY_PANELS; x++)
        {
            

            // Code that will fill panel with information from quiz template goes here.

            GamePanel categoryPanelInstance = Instantiate(gamePanelPrefab, categoryPanelsGroup.transform);

            categoryPanelInstance.panelType = GamePanel.PanelType.Category;
            categoryPanelInstance.panelXCoordinate = x;
            categoryPanelInstance.panelYCoordinate = 0;

            // The y-coordinate for category panels is set to 0 as there is no vertical placement of these panels.

            //categoryPanelInstance.panelGroup = 0;
            //categoryPanelInstance.panelNumb = i;
        }
    }

    void CreateQuestionPanels(GamePanel questionPanel)
    {
        // TODO: Rework code here to just load and instantiate game panels. The Grid Layout Group component in the parent object takes care of the layout.

        for (int y = 0; y < MAXIMUM_QUESTION_PANELS_PER_COLUMN; y++)
        {
            for (int x = 0; x < MAXIMUM_QUESTION_PANELS_PER_ROW; x++)
            {
                

                // Code that will fill panel with information from quiz template goes here.

                GamePanel questionPanelInstance = Instantiate(gamePanelPrefab, questionPanelsGroup.transform);

                questionPanelInstance.panelType = GamePanel.PanelType.Question;
                questionPanelInstance.panelXCoordinate = x;
                questionPanelInstance.panelYCoordinate = y;
                questionPanelInstance.panelPointValue = gamePanelPrefab.panelPointValue;

                //questionPanelInstance.panelGroup = 1;
                //questionPanelInstance.panelNumb = i;
            }

            // Code after a row is completed goes here.
        }
    }
}
