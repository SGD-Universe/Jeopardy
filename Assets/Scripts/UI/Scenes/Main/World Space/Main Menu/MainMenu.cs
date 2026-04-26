using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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

    string quizTemplatePath;
    int quizTemplateCount;

    // TODO: Check if the game has at least 1 quiz template. If not, disable the New Game and Load Game buttons because you cannot start a game without one.

    void Awake()
    {
        quizTemplatePath = Application.persistentDataPath + "/QuizTemplates";
        quizTemplateCount = CountQuizTemplateJsonFiles(quizTemplatePath); // Get the number of JSON files in the quiz template path

        //Debug.Log("Number of quiz template JSON files in the quiz template folder: " + quizTemplateCount);
    }

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
        // If the quiz template folder does not exist...
        if (!Directory.Exists(quizTemplatePath))
        {
            // ...create a new quiz template folder...

            Directory.CreateDirectory(quizTemplatePath);

            //Debug.Log("Directory was not found. Quiz template folder created at: " + quizTemplatePath);
        }
        // ...otherwise...
        else
        {
            // ...move on with the rest of Start()

            //Debug.Log("Quiz template folder already exists at: " + quizTemplatePath);
        }

        // If there are no quiz template JSON files...
        if (quizTemplateCount == 0)
        {
            // ...disable the New Game and Load Game buttons...
            //Debug.Log("JSON files are not found in the quiz template folder. Disabling the New Game and Load Game buttons!");

            newGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
        // ...otherwise...
        else
        {
            // ...enable the New Game and Load Game buttons
            //Debug.Log("JSON files have been found in the quiz template folder. Enabling the New Game and Load Game buttons!");

            newGameButton.interactable = true;
            loadGameButton.interactable = true;
        }
    }

    int CountQuizTemplateJsonFiles(string quizTemplateFolderPath)
    {
        string[] quizTemplateFiles = Directory.GetFiles(quizTemplateFolderPath, "*.json");

        return quizTemplateFiles.Length;
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
