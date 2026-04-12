using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
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
                Debug.Log("QUIT MENU: QuitGame listener added!");
            }

            if (backButton != null)
            {
                backButton.onClick.AddListener(CloseQuitMenu);
                Debug.Log("QUIT MENU: CloseQuitMenu listener added!");
            }

            listenersAdded = true;
        }
    }

    void OnDisable()
    {
        if (yesButton != null)
        {
            yesButton.onClick.RemoveListener(QuitGame);
            Debug.Log("QUIT MENU: QuitGame listener removed!");
        }

        if (backButton != null)
        {
            backButton.onClick.RemoveListener(CloseQuitMenu);
            Debug.Log("QUIT MENU: CloseQuitMenu listener removed!");
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
}
