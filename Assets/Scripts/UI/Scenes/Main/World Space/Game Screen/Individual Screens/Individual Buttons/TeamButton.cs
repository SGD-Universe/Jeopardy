using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class TeamButton : MonoBehaviour
{
    [SerializeField] private GamePanel selectedPanel;
    [SerializeField] private Team linkedTeam;
    [SerializeField] private Button linkedButton;
    [SerializeField] private TextMeshProUGUI teamButtonText;

    [SerializeField] private TeamOptionsScreen teamOptionsScreen;

    void Awake()
    {
        teamButtonText.text = "Team " + linkedTeam.teamNumber;
    }

    void OnEnable()
    {
        linkedButton.onClick.AddListener(OpenTeamOptionsScreen);
    }

    void OnDisable()
    {
        linkedButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenTeamOptionsScreen()
    {
        teamOptionsScreen.teamNameText.text = teamButtonText.text;
        teamOptionsScreen.selectedPanelPointValueText.text = "$" + string.Format(CultureInfo.InvariantCulture, "{0:N0}", selectedPanel.panelPointValue);

        if (!teamOptionsScreen.gameObject.activeInHierarchy)
        {
            teamOptionsScreen.gameObject.SetActive(true);
        }
    }
}
