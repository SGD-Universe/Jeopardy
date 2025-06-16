using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ExperimentalCreationController : MonoBehaviour
{
    [SerializeField] private GameObject panelTemplate;
    [SerializeField] private Vector2 panelSpacing = new Vector2(4, 3);
    [SerializeField] private TMP_InputField titleInput;
    [SerializeField] private float titleInputColorLerpFactor = 0f;
    [SerializeField] private CanvasGroup warningPanel;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private float warningPanelAlphaLerpFactor = 0f;
    [SerializeField] private Color errorColor;

    private Color titleInputOriginalColor;

    private Vector2 panelCount = new Vector2(6, 6);
    private List<List<MonitorPlane>> panelsBoard = new List<List<MonitorPlane>>();

    private Animator animator;
    
    [System.Serializable]
    private struct PanelData {
        public bool isCategory;
        public string primaryText;
        public string secondaryText;
    }

    [System.Serializable]
    private struct ColumnData
    {
        public List<PanelData> panels;
    }

    private struct BoardData
    {
        public List<ColumnData> columns;
    }

    private BoardData boardData;

    // Start is called before the first frame update
    void Start()
    {
        for(int c  = 0; c < panelCount.x; c ++)
        {
            List<MonitorPlane> panelsColumn = new List<MonitorPlane>();
            for(int r  = 0; r < panelCount.y; r ++)
            {
                
                // float spawnX = (c + 0.5f - panelCount.x / 2f) * panelTemplate.transform.localScale.x * panelSpacing.x;
                // float spawnY = (r + 0.5f - panelCount.y / 2f) * panelTemplate.transform.localScale.y * panelSpacing.y;
                // Spawns from top-to-bottom, left-to-right
                float spawnX = ((c + 0.5f) - (panelCount.x / 2f)) * panelTemplate.transform.localScale.x * panelSpacing.x;
                float spawnY = ((panelCount.y / 2f) - (r + 0.5f)) * panelTemplate.transform.localScale.y * panelSpacing.y;
                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
                
                GameObject panelClone = Instantiate(panelTemplate, transform);
                panelClone.transform.localPosition = spawnPosition;
                
                MonitorPlane monitorPlane = panelClone.GetComponent<MonitorPlane>();
                if(r == 0) monitorPlane.SetPanelType(MonitorPlane.Type.Category);
                else monitorPlane.SetPanelType(MonitorPlane.Type.Question);
                panelsColumn.Add(monitorPlane);
            }
            panelsBoard.Add(panelsColumn);
        }
        panelTemplate.SetActive(false);

        titleInputOriginalColor = titleInput.colors.normalColor;

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) SaveBoardData();
        if(Input.GetKeyDown(KeyCode.F5)) FillBoardWithDummyData();
        if(Input.GetKeyDown(KeyCode.F9)) EraseBoardData();

        UnityEngine.UI.ColorBlock titleInputColorBlockClone = titleInput.colors;
        titleInputColorBlockClone.normalColor = Color.Lerp(titleInputOriginalColor, errorColor, titleInputColorLerpFactor);
        titleInput.colors = titleInputColorBlockClone;

        warningPanel.alpha = warningPanelAlphaLerpFactor;
    }

    private void SaveBoardData()
    {
        if(VerifyBoardData())
        {
            boardData = new BoardData();
            boardData.columns = new List<ColumnData>();
            foreach(List<MonitorPlane> panelsColumn in panelsBoard)
            {
                ColumnData currentColumnData = new ColumnData();
                currentColumnData.panels = new List<PanelData>();
                foreach(MonitorPlane panel in panelsColumn)
                {
                    string string1 = panel.GetPrimaryInputString();
                    string string2 = panel.GetSecondaryInputString();

                    PanelData currentPanelData = new PanelData();
                    currentPanelData.primaryText = string1;
                    if(panel.GetPanelType() == MonitorPlane.Type.Category) currentPanelData.isCategory = true;
                    else currentPanelData.secondaryText = string2;
                    currentColumnData.panels.Add(currentPanelData);
                }
                boardData.columns.Add(currentColumnData);
            }
            string json = JsonUtility.ToJson(boardData, true);
            string fileName = titleInput.text.Trim();
            char[] invalid = Path.GetInvalidFileNameChars();
            if(string.IsNullOrWhiteSpace(fileName))
            {
                animator.Play("CreationScreenTitleError", 0, 0f);
                animator.Play("CreationScreenWarningFlash", 1, 0f);
                warningText.text = "Please give your quiz a title";
                return;
            }
            foreach(char c in fileName)
            {
                if(invalid.Contains(c))
                {
                    animator.Play("CreationScreenTitleError", 0, 0f);
                    animator.Play("CreationScreenWarningFlash", 1, 0f);
                    warningText.text = "Quiz title has invalid characters. Please give it a different name.";
                    return;
                }
            }
            string filePath = Application.persistentDataPath + "/" + fileName + ".json";
            File.WriteAllText(filePath, json);
            Process.Start("explorer.exe", "/select,\"" + Path.GetFullPath(filePath) + "\"");
        }
    }

    private bool VerifyBoardData()
    {
        bool dataIsValid = true;
        foreach(List<MonitorPlane> panelsColumn in panelsBoard)
        {
            foreach(MonitorPlane panel in panelsColumn)
            {
                string string1 = panel.GetPrimaryInputString();
                string string2 = panel.GetSecondaryInputString();
                if(string.IsNullOrEmpty(string1))
                {
                    panel.FlashError();
                    dataIsValid = false;
                }
                else if(panel.GetPanelType() == MonitorPlane.Type.Question && string.IsNullOrEmpty(string2))
                {
                    panel.FlashError();
                    dataIsValid = false;
                }
            }
        }
        if(dataIsValid == false)
        {
            animator.Play("CreationScreenWarningFlash", 1, 0f);
            warningText.text = "Please fill out all the panels";
        }
        return dataIsValid;
    }

    private void FillBoardWithDummyData()
    {
        foreach(List<MonitorPlane> panelsColumn in panelsBoard)
        {
            foreach(MonitorPlane panel in panelsColumn)
            {
                if(panel.GetPanelType() == MonitorPlane.Type.Category)
                {
                    panel.SetPrimaryInputString("SHOES");
                }
                else if(panel.GetPanelType() == MonitorPlane.Type.Question)
                {
                    panel.SetPrimaryInputString("These shoes are named after Florida's iconic reptile");
                    panel.SetSecondaryInputString("What are Crocs?");
                }
            }
        }
    }

    private void EraseBoardData()
    {
        foreach(List<MonitorPlane> panelsColumn in panelsBoard)
        {
            foreach(MonitorPlane panel in panelsColumn)
            {
                panel.SetPrimaryInputString("");
                panel.SetSecondaryInputString("");
            }
        }
    }
}
