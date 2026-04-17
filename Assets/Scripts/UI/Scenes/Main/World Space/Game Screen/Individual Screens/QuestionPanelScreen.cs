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

    [SerializeField] private GameObject closeQuestionWarningScreen; // TODO: Create a close question warning screen object!

    void Awake()
    {
        
    }

    void OnEnable()
    {
        exitQuestionButton.onClick.AddListener(CloseQuestionPanelScreen);
        closeQuestionButton.onClick.AddListener(OpenCloseQuestionWarningScreen);
    }

    void OnDisable()
    {
        exitQuestionButton.onClick.RemoveAllListeners();
        closeQuestionButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateTeamButtons(teamButtonPrefab);
    }

    public void CreateTeamButtons(TeamButton teamButton)
    {
        for (int i = 0; i < gameManager.teamCount; i++)
        {
            // Code that will tie teams to each team button goes here.

            TeamButton teamButtonInstance = Instantiate(teamButtonPrefab, teamButtonsGroup.transform);
        }
    }

    public void CloseQuestionPanelScreen()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenCloseQuestionWarningScreen()
    {

    }
}
