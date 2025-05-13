using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QAPanelController : MonoBehaviour
{
    public GameObject[] scenePanels;  // panel references
    public Button closeButton;

    void Start()
    {
        // deactivates all panels at start
        foreach (GameObject panel in scenePanels)
        {
            panel.SetActive(false);
        }
    }

    public void OpenPanel(int levelId)
    {
        //makes sure all panels are deactivated
        foreach (GameObject panel in scenePanels)
        {
            panel.SetActive(false);
        }

        // activate new panel and refresh listeners
        if (levelId >= 0 && levelId < scenePanels.Length)
        {
            scenePanels[levelId].SetActive(true);
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => ClosePanel(levelId));
        }
    }

    public void ClosePanel(int levelId)
    {
        // Deactivates panel
        if (levelId >= 0 && levelId < scenePanels.Length)
        {
            scenePanels[levelId].SetActive(false);
        }
    }
}
