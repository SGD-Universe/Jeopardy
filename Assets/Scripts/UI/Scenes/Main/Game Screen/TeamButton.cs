using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class TeamButton : MonoBehaviour
{
    public Team linkedTeam;

    [SerializeField] private Button linkedButton;
    [SerializeField] private TextMeshProUGUI teamButtonText;

    [SerializeField] private TeamOptionsScreen teamOptionsScreen;

    void Awake()
    {
        teamButtonText.text = linkedTeam.teamName;
    }

    void OnEnable()
    {
        linkedButton.onClick.AddListener(OpenTeamOptionsScreen);
    }

    void OnDisable()
    {
        linkedButton.onClick.RemoveAllListeners();
    }

    public void OpenTeamOptionsScreen()
    {
        if (!teamOptionsScreen.gameObject.activeInHierarchy)
            teamOptionsScreen.gameObject.SetActive(true);
    }
}