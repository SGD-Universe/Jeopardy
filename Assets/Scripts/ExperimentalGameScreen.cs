using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalGameScreen : MonoBehaviour
{
    public static ExperimentalGameScreen Instance;

    [SerializeField] private GameObject QuestionPanelScreen;
    [SerializeField] private GameObject OverviewScreen;
    [SerializeField] private GameObject CategoryIntroductionScreen;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenQuestionPanel()
    {
        OverviewScreen.SetActive(false);
        QuestionPanelScreen.SetActive(true);
    }

    public void ReturnToOverviewScreen()
    {
        CategoryIntroductionScreen.SetActive(false);
        QuestionPanelScreen.SetActive(false);
        OverviewScreen.SetActive(true);
    }

    public void BeginGame()
    {
        CategoryIntroductionScreen.GetComponent<ExperimentalCategoryIntroduction>().BeginIntroduction();
    }
}
