using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreationScreenController : MonoBehaviour
{
    [Header("Panel Setup")]
    [SerializeField] private GameObject panelTemplate;
    [SerializeField] private Vector2 panelSpacing = new Vector2(4f, 3f);
    [Tooltip("Number of columns (x) and rows (y) in the board.")]
    [SerializeField] private int columns = 6;
    [SerializeField] private int rows = 6;

    [Header("UI References")]
    [SerializeField] private TMP_InputField titleInput;
    [SerializeField] private float titleInputColorLerpFactor = 0f;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private Color errorColor = Color.red;
    [SerializeField] private GameObject eraseButton;
    [SerializeField] private GameObject fillButton;

    private Color titleInputOriginalColor;

    private readonly List<List<MonitorPlane>> panelsBoard = new List<List<MonitorPlane>>();
    private Animator animator;
    private SaveManager.BoardData boardData;

    private void Start()
    {
        // Basic safety checks
        if (panelTemplate == null)
        {
            Debug.LogError("[CreationScreenController] Panel template is not assigned.");
            enabled = false;
            return;
        }

        if (titleInput == null)
        {
            Debug.LogWarning("[CreationScreenController] Title input field is not assigned.");
        }

        if (warningText == null)
        {
            Debug.LogWarning("[CreationScreenController] Warning text is not assigned.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("[CreationScreenController] Animator component is missing.");
        }

        // Store original title color (if assigned)
        if (titleInput != null)
        {
            titleInputOriginalColor = titleInput.colors.normalColor;
        }

        GenerateBoard();
    }

    private void GenerateBoard()
    {
        panelsBoard.Clear();

        // Ensure template itself is hidden (only clones are visible)
        panelTemplate.SetActive(false);

        for (int c = 0; c < columns; c++)
        {
            List<MonitorPlane> panelsColumn = new List<MonitorPlane>();

            for (int r = 0; r < rows; r++)
            {
                float spawnX = ((c + 0.5f) - (columns / 2f)) *
                               panelTemplate.transform.localScale.x * panelSpacing.x;

                float spawnY = ((rows / 2f) - (r + 0.5f)) *
                               panelTemplate.transform.localScale.y * panelSpacing.y;

                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

                GameObject panelClone = Instantiate(panelTemplate, transform);
                panelClone.transform.localPosition = spawnPosition;
                panelClone.SetActive(true);

                MonitorPlane monitorPlane = panelClone.GetComponent<MonitorPlane>();
                if (monitorPlane == null)
                {
                    Debug.LogError("[CreationScreenController] Panel clone is missing MonitorPlane component.");
                    continue;
                }

                // First row is categories, the rest are questions
                if (r == 0)
                    monitorPlane.SetPanelType(MonitorPlane.Type.Category);
                else
                    monitorPlane.SetPanelType(MonitorPlane.Type.Question);

                panelsColumn.Add(monitorPlane);
            }

            panelsBoard.Add(panelsColumn);
        }
    }

    private void Update()
    {
        // Toggle erase/fill buttons with F1
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (eraseButton != null)
                eraseButton.SetActive(!eraseButton.activeInHierarchy);

            if (fillButton != null)
                fillButton.SetActive(!fillButton.activeInHierarchy);
        }

        // Animate title input color (driven by animations changing titleInputColorLerpFactor)
        if (titleInput != null)
        {
            UnityEngine.UI.ColorBlock titleInputColorBlockClone = titleInput.colors;
            titleInputColorBlockClone.normalColor =
                Color.Lerp(titleInputOriginalColor, errorColor, titleInputColorLerpFactor);
            titleInput.colors = titleInputColorBlockClone;
        }
    }

    public void SaveBoardData()
    {
        if (!VerifyBoardData())
            return;

        boardData = new SaveManager.BoardData();

        int c = 0;
        foreach (List<MonitorPlane> panelsColumn in panelsBoard)
        {
            foreach (MonitorPlane panel in panelsColumn)
            {
                bool isCategory = (panel.GetPanelType() == MonitorPlane.Type.Category);
                string string1 = panel.GetPrimaryInputString();
                string string2 = panel.GetSecondaryInputString();
                boardData.AddPanel(c, isCategory, string1, string2);
            }

            c++;
        }

        string fileName = (titleInput != null ? titleInput.text : string.Empty).Trim();
        int saveValidity = SaveManager.SaveBoardData(boardData, fileName);

        if (saveValidity == -1)
        {
            PlayTitleError("Please give your quiz a title");
        }
        else if (saveValidity == -2)
        {
            PlayTitleError("Quiz title has invalid characters. Please give it a different name.");
        }
        else if (saveValidity == 0)
        {
            // Turn off warning flash and play success flash
            if (animator != null)
            {
                animator.Play("CreationScreenIdle", 1, 0f);
                animator.Play("CreationScreenSuccessFlash", 2, 0f);
            }
        }
    }

    private void PlayTitleError(string message)
    {
        if (animator != null)
        {
            animator.Play("CreationScreenTitleError", 0, 0f);
            animator.Play("CreationScreenWarningFlash", 1, 0f);
        }

        if (warningText != null)
        {
            warningText.text = message;
        }
    }

    private bool VerifyBoardData()
    {
        bool dataIsValid = true;

        foreach (List<MonitorPlane> panelsColumn in panelsBoard)
        {
            foreach (MonitorPlane panel in panelsColumn)
            {
                string string1 = panel.GetPrimaryInputString();
                string string2 = panel.GetSecondaryInputString();

                if (string.IsNullOrWhiteSpace(string1))
                {
                    panel.FlashError();
                    dataIsValid = false;
                }
                else if (panel.GetPanelType() == MonitorPlane.Type.Question &&
                         string.IsNullOrWhiteSpace(string2))
                {
                    panel.FlashError();
                    dataIsValid = false;
                }
            }
        }

        if (!dataIsValid)
        {
            if (animator != null)
            {
                animator.Play("CreationScreenWarningFlash", 1, 0f);
            }

            if (warningText != null)
            {
                warningText.text = "Please fill out all the panels";
            }
        }

        return dataIsValid;
    }

    public void FillBoardWithDummyData()
    {
        foreach (List<MonitorPlane> panelsColumn in panelsBoard)
        {
            foreach (MonitorPlane panel in panelsColumn)
            {
                if (panel.GetPanelType() == MonitorPlane.Type.Category)
                {
                    panel.SetPrimaryInputString("SHOES");
                }
                else if (panel.GetPanelType() == MonitorPlane.Type.Question)
                {
                    panel.SetPrimaryInputString("These shoes are named after Florida's iconic reptile");
                    panel.SetSecondaryInputString("What are Crocs?");
                }
            }
        }
    }

    public void EraseBoardData()
    {
        foreach (List<MonitorPlane> panelsColumn in panelsBoard)
        {
            foreach (MonitorPlane panel in panelsColumn)
            {
                panel.SetPrimaryInputString(string.Empty);
                panel.SetSecondaryInputString(string.Empty);
            }
        }
    }
}
