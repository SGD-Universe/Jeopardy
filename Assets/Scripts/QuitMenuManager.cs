using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenuManager : MonoBehaviour
{
    public Button yesButton;
    public Button backButton;

    private bool listenersAdded = false;

    void OnEnable()
    {
        if (!listenersAdded)
        {
            if (yesButton != null)
            {
                yesButton.onClick.AddListener(QuitGame);
            }

            if (backButton != null)
            {
                backButton.onClick.AddListener(CloseQuitMenu);
            }

            listenersAdded = true;
        }
    }

    public void ShowQuitMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseQuitMenu()
    {
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnDestroy()
    {
        if (yesButton != null)
        {
            yesButton.onClick.RemoveListener(QuitGame);
        }

        if (backButton != null)
        {
            backButton.onClick.RemoveListener(CloseQuitMenu);
        }
    }
}
