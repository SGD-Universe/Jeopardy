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

    [Header("Game Manager Component")]
    [SerializeField] private GameManager gameManager;

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
        CreateQuizPanels(gamePanelPrefab);
    }

    void CreateQuizPanels(GamePanel panelPrefab)
    {
        CreateCategoryPanels(panelPrefab);
        CreateQuestionPanels(panelPrefab);
    }

    // TODO: Implement the instantiation of game panels in a grid using the Grid Layout Group components attached to the Category Panels and Quiz Panels GameObjects.

    void CreateCategoryPanels(GamePanel categoryPanel)
    {
        for (int i = 0; i < MAXIMUM_CATEGORY_PANELS; i++)
        {
            categoryPanel.panelType = GamePanel.PanelType.Category;
            categoryPanel.panelGroup = 0;
            categoryPanel.panelNumb = i;

            // Code that will fill panel with information from quiz template goes here.

            GamePanel categoryPanelInstance = Instantiate(gamePanelPrefab, categoryPanelsGroup.transform);
        }
    }

    void CreateQuestionPanels(GamePanel questionPanel)
    {
        // TODO: Rework code here to just load and instantiate game panels. The Grid Layout Group component in the parent object takes care of the layout.

        for (int i = 0; i < MAXIMUM_QUESTION_PANELS_PER_COLUMN; i++)
        {
            for (int j = 0; j < MAXIMUM_QUESTION_PANELS_PER_ROW; j++)
            {
                questionPanel.panelType = GamePanel.PanelType.Question;
                questionPanel.panelPointValue = gamePanelPrefab.panelPointValue;
                questionPanel.panelGroup = 1;
                questionPanel.panelNumb = i;

                // Code that will fill panel with information from quiz template goes here.

                GamePanel questionPanelInstance = Instantiate(gamePanelPrefab, questionPanelsGroup.transform);
            }

            // Code after a row is completed goes here.
        }
    }
}
