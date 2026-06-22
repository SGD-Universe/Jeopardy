using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    string quizTemplatePath;
    int quizTemplateCount;

    // TODO: Check if the game has at least 1 quiz template. If not, disable the New Game and Load Game buttons because you cannot start a game without one.

    void Awake()
    {
        quizTemplatePath = Application.streamingAssetsPath + "/QuizTemplates";
        quizTemplateCount = CountQuizTemplateJsonFiles(quizTemplatePath); // Get the number of JSON files in the quiz template path

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

    }

    void Start()
    {
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
}
