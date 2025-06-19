using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CreationScreenController : MonoBehaviour
{
    [SerializeField] private GameObject panelTemplate;
    [SerializeField] private Vector2 panelSpacing = new Vector2(4, 3);
    [SerializeField] private TMP_InputField titleInput;
    [SerializeField] private float titleInputColorLerpFactor = 0f;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private Color errorColor;
    [SerializeField] private GameObject eraseButton;
    [SerializeField] private GameObject fillButton;

    private Color titleInputOriginalColor;

    private Vector2 panelCount = new Vector2(6, 6);
    private List<List<MonitorPlane>> panelsBoard = new List<List<MonitorPlane>>();

    private Animator animator;

    private SaveManager.BoardData boardData;


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
        if(Input.GetKeyDown(KeyCode.F1))
        {
            eraseButton.SetActive(!eraseButton.activeInHierarchy);
            fillButton.SetActive(!fillButton.activeInHierarchy);
        }

        UnityEngine.UI.ColorBlock titleInputColorBlockClone = titleInput.colors;
        titleInputColorBlockClone.normalColor = Color.Lerp(titleInputOriginalColor, errorColor, titleInputColorLerpFactor);
        titleInput.colors = titleInputColorBlockClone;
    }

    public void SaveBoardData()
    {
        if(VerifyBoardData())
        {
            boardData = new SaveManager.BoardData();
            int c = 0;
            foreach(List<MonitorPlane> panelsColumn in panelsBoard)
            {
                foreach(MonitorPlane panel in panelsColumn)
                {
                    bool isCategory = (panel.GetPanelType() == MonitorPlane.Type.Category);
                    string string1 = panel.GetPrimaryInputString();
                    string string2 = panel.GetSecondaryInputString();
                    boardData.AddPanel(c, isCategory, string1, string2);
                }
                c++;
            }

            string fileName = titleInput.text.Trim();
            int saveValidity = SaveManager.SaveBoardData(boardData, fileName);


            if(saveValidity == -1)
            {
                animator.Play("CreationScreenTitleError", 0, 0f);
                animator.Play("CreationScreenWarningFlash", 1, 0f);
                warningText.text = "Please give your quiz a title";
                return;
            }
            else if(saveValidity == -2)
            {
                animator.Play("CreationScreenTitleError", 0, 0f);
                animator.Play("CreationScreenWarningFlash", 1, 0f);
                warningText.text = "Quiz title has invalid characters. Please give it a different name.";
                return;
            }
            else if(saveValidity == 0)
            {
                // Turn off warning flash and play success flash
                animator.Play("CreationScreenIdle", 1, 0f);
                animator.Play("CreationScreenSuccessFlash", 2, 0f);
            }
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

    public void FillBoardWithDummyData()
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

    public void EraseBoardData()
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
