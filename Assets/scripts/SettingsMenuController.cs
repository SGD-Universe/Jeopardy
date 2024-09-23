using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public static SettingsMenuController Instance;
    [Header("Settings Menu")]
    public GameObject settingsMenu;
    public Button backButton;
    public Button mainMenuButton;
    public Button settingsMenuButton;


    public void Start()
    {
        // SettingsMenu starts inactive
        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        // Add listeners
        settingsMenuButton.onClick.AddListener(OpenSettings);
        backButton.onClick.AddListener(CloseSettingsMenu);
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadSceneByName("MainMenu"));
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
/*
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CreateGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
*/
}
