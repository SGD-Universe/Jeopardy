using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionPanelScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI panelQuestionText;

    // This is the point where the first Team Button will be instantiated. Other Team Buttons will be instantiated relative to this point.
    [SerializeField] private Transform firstTeamButtonPoint;

    [SerializeField] private TeamButton teamButtonPrefab;

    [SerializeField] private List<TeamButton> teamButtonsList = new List<TeamButton>(); // Using a List to instantiate the proper number of team buttons based on team count.

    [SerializeField] private Button exitQuestionButton;
    [SerializeField] private Button closeQuestionButton;

    [SerializeField] private GameObject closeQuestionWarningScreen;

    [SerializeField] private float teamButtonHorizontalOffset;

    void Awake()
    {
        
    }

    void OnEnable()
    {
        

        for (int i = 0; i < gameManager.teamCount; i++)
        {
            Instantiate(teamButtonPrefab.gameObject, new Vector3(firstTeamButtonPoint.localPosition.x + (teamButtonHorizontalOffset * i), 0f, 0f), Quaternion.identity);
        }

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
        for (int i = 0; i < gameManager.teamCount;  i++)
        {
            

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
