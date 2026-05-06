using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    GameManager gameManager;

    [Header("Main Menu Buttons")]
    public Button newGameButton;
    public Button loadGameButton;
    public Button createEditQuizButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Screens")]
    [SerializeField] private GameObject newGameScreen;
    [SerializeField] private GameObject loadGameScreen;
    [SerializeField] private GameObject createEditQuizScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject quitScreen;

    void OnEnable()
    {
        newGameButton.onClick.AddListener(() => HideMenu(gameObject)); // This will allow functions with parameters to be added into events.
        newGameButton.onClick.AddListener(() => ShowMenu(newGameScreen));

        loadGameButton.onClick.AddListener(() => HideMenu(gameObject));
        loadGameButton.onClick.AddListener(() => ShowMenu(loadGameScreen));

        createEditQuizButton.onClick.AddListener(() => HideMenu(gameObject));
        createEditQuizButton.onClick.AddListener(() => ShowMenu(createEditQuizScreen));

        settingsButton.onClick.AddListener(() => HideMenu(gameObject));
        settingsButton.onClick.AddListener(() => ShowMenu(settingsScreen));

        quitButton.onClick.AddListener(() => HideMenu(gameObject));
        quitButton.onClick.AddListener(() => ShowMenu(quitScreen));
    }

    void OnDisable()
    {
        newGameButton.onClick.RemoveAllListeners();

        loadGameButton.onClick.RemoveAllListeners();

        createEditQuizButton.onClick.RemoveAllListeners();

        settingsButton.onClick.RemoveAllListeners();

        quitButton.onClick.RemoveAllListeners();
    }

    void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager.quizTemplateCount <= 0)
        {
            // A new game cannot be started without a quiz template.

            newGameButton.interactable = false;
        }
        else
        {
            newGameButton.interactable = true;
        }

        if (gameManager.savedGameCount <= 0)
        {
            // A game cannot be loaded without a saved game file.

            loadGameButton.interactable = false;
        }
        else
        {
            loadGameButton.interactable = true;
        }
    }

    void HideMenu(GameObject menuObject)
    {
        menuObject.SetActive(false);
    }

    void ShowMenu(GameObject menuObject)
    {
        menuObject.SetActive(true);
    }
}
