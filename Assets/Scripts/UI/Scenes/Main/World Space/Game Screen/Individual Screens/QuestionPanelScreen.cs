using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionPanelScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI panelQuestionText;

    [SerializeField] private GameObject teamButtonsGroup;

    [SerializeField] private TeamButton teamButtonPrefab;

    [SerializeField] private Button exitQuestionButton;
    [SerializeField] private Button closeQuestionButton;
    [SerializeField] private Button confirmCloseButton;
    [SerializeField] private Button cancelCloseButton;

    private GamePanel currentPanel;

    [SerializeField] private GameObject closeQuestionWarningScreen; // TODO: Create a close question warning screen object!

    void Awake()
    {
        
    }

    void OnEnable()
    {
        if (exitQuestionButton != null)
            exitQuestionButton.onClick.AddListener(CloseQuestionPanelScreen);
        else
            Debug.LogWarning("QuestionPanelScreen: exitQuestionButton is not assigned in the Inspector.");

        if (closeQuestionButton != null)
            closeQuestionButton.onClick.AddListener(OpenCloseQuestionWarningScreen);
        else
            Debug.LogWarning("QuestionPanelScreen: closeQuestionButton is not assigned in the Inspector.");

        if (confirmCloseButton != null)
            confirmCloseButton.onClick.AddListener(CloseQuestionPanelScreen);
        else
            Debug.LogWarning("QuestionPanelScreen: confirmCloseButton is not assigned in the Inspector.");

        if (cancelCloseButton != null)
            cancelCloseButton.onClick.AddListener(CancelCloseQuestion);
        else
            Debug.LogWarning("QuestionPanelScreen: cancelCloseButton is not assigned in the Inspector.");
    }

    void OnDisable()
    {
        if (exitQuestionButton != null) exitQuestionButton.onClick.RemoveAllListeners();
        if (closeQuestionButton != null) closeQuestionButton.onClick.RemoveAllListeners();
        if (confirmCloseButton != null) confirmCloseButton.onClick.RemoveAllListeners();
        if (cancelCloseButton != null) cancelCloseButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (teamButtonPrefab != null && gameManager != null)
        {
            CreateTeamButtons(teamButtonPrefab);
        }
        else
        {
            if (gameManager == null)
                Debug.LogWarning("QuestionPanelScreen: gameManager is not assigned in the Inspector.");
            if (teamButtonPrefab == null)
                Debug.LogWarning("QuestionPanelScreen: teamButtonPrefab is not assigned in the Inspector.");
        }
    }

    public void CreateTeamButtons(TeamButton teamButton)
    {
        if (gameManager == null || teamButtonsGroup == null) return;

        for (int i = 0; i < gameManager.teamCount; i++)
        {
            // Code that will tie teams to each team button goes here.

            TeamButton teamButtonInstance = Instantiate(teamButtonPrefab, teamButtonsGroup.transform);
        }
    }
    public void ShowQuestion(GamePanel panel)
    {
        currentPanel = panel;
        if (panelQuestionText != null)
            panelQuestionText.text = panel.panelText_Primary;
        else
            Debug.LogWarning("QuestionPanelScreen: panelQuestionText is not assigned — cannot display question text.");
    }

    public void OpenQuestion()
    {
        ShowQuestion(currentPanel);
        gameObject.SetActive(true);
    }
    
    public void CloseQuestionPanelScreen()
    {
        this.gameObject.SetActive(false);
        
    }

    public void OpenCloseQuestionWarningScreen()
    {
        if (closeQuestionWarningScreen != null)
            closeQuestionWarningScreen.SetActive(true);
        else
            Debug.LogWarning("QuestionPanelScreen: closeQuestionWarningScreen is not assigned in the Inspector.");
    }

    void ConfirmCloseQuestion()
    {
       if(currentPanel != null)
       {
        currentPanel.CloseQuestion();
       } 
       if (closeQuestionWarningScreen != null)
           closeQuestionWarningScreen.SetActive(false);
       CloseQuestionPanelScreen();
    }

    void CancelCloseQuestion()
    {
       if (closeQuestionWarningScreen != null)
           closeQuestionWarningScreen.SetActive(false); 
    }
}
