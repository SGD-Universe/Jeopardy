using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionPanelScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI panelQuestionText;

    [SerializeField] private Button teamButton;
    [SerializeField] private Button exitQuestionButton;
    [SerializeField] private Button closeQuestionButton;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameManager.teamCount;  i++)
        {
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
