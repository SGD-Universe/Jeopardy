using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    public Button createGameButton;
    public Button loadGameButton;
    public Button quitButton;
    [SerializeField] private GameObject settingsMenu;


    private void Start()
    {
        // add listeners
        if (createGameButton != null)
            createGameButton.onClick.AddListener(() => GameManager.Instance.LoadSceneByName("CreateGame"));

        if (loadGameButton != null)
            loadGameButton.onClick.AddListener(() => GameManager.Instance.LoadSceneByName("LoadGame"));

        if (quitButton != null)
            quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }

    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void HideSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
}
