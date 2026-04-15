using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeamOptionsScreen : MonoBehaviour
{
    public TextMeshProUGUI teamNameText;
    public TextMeshProUGUI selectedPanelPointValueText;

    [SerializeField] private Button correctButton;
    [SerializeField] private Button incorrectButton;
    [SerializeField] private Button cancelButton;

    void Awake()
    {
        //teamNameText.text = 
    }

    void OnEnable()
    {
        cancelButton.onClick.AddListener(CloseTeamOptionsScreen);
    }

    void OnDisable()
    {
        cancelButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseTeamOptionsScreen()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
        }
    }
}
