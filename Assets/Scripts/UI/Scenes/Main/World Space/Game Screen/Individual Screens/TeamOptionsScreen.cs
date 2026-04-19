using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class TeamOptionsScreen : MonoBehaviour
{
    [SerializeField] private GamePanel selectedGamePanel;
    [SerializeField] TeamButton selectedTeamButton;

    public TextMeshProUGUI teamNameText;
    public TextMeshProUGUI selectedPanelPointValueText;

    [SerializeField] private Button correctButton;
    [SerializeField] private Button incorrectButton;
    [SerializeField] private Button cancelButton;

    void Awake()
    {
        teamNameText.text = selectedTeamButton.linkedTeam.teamName;
        selectedPanelPointValueText.text = "$" + string.Format(CultureInfo.InvariantCulture, "{0:N0}", selectedGamePanel.panelPointValue);
    }

    void OnEnable()
    {
        //correctButton.onClick.AddListener();

        //incorrectButton.onClick.AddListener();

        cancelButton.onClick.AddListener(CloseTeamOptionsScreen);
    }

    void OnDisable()
    {
        correctButton.onClick.RemoveAllListeners();

        incorrectButton.onClick.RemoveAllListeners();

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
