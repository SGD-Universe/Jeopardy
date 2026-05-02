using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMenu : MonoBehaviour
{
    // TODO: Clicking on the Create Quiz Button should disable the Main Menu Virtual Camera and load an empty panel grid, ready to be edited.

    /* TODO: Clicking on the Edit Quiz Button should open a menu similar to the Load Quizzes Menu, with quiz template buttons created
     * depending on the number of quiz templates found in the Quiz Templates folder.
     */

    /* TODO: Clicking on the Import Quiz Button should open File Explorer (in file select mode) at the Quiz Templates folder. When a file is selected,
     * disable the Main Menu Virtual Camera, load the panel grid, and load the template data into the respective panels.
     */

    [Header("Quiz Menu Buttons")]
    public Button createQuizButton;
    public Button editQuizButton;
    public Button importQuizButton;
    public Button backButton;

    void OnEnable()
    {
        if (createQuizButton != null)
        {
            createQuizButton.onClick.AddListener(LoadCreateQuizScene);
            UnityEngine.Debug.Log("QUIZ MENU ON ENABLE: LoadCreateQuizScene listener added!");
        }

        if (editQuizButton != null)
        {
            editQuizButton.onClick.AddListener(LoadEditQuizScene);
            UnityEngine.Debug.Log("QUIZ MENU ON ENABLE: LoadEditQuizScene listener added!");
        }

        if (importQuizButton != null)
        {
            importQuizButton.onClick.AddListener(ImportQuizFile);
            UnityEngine.Debug.Log("QUIZ MENU ON ENABLE: ImportQuizFile listener added!");
        }

        if (backButton != null)
        {

        }
    }

    void OnDisable()
    {
        if (createQuizButton != null)
        {
            createQuizButton.onClick.RemoveAllListeners();
            UnityEngine.Debug.Log("QUIZ MENU ON DISABLE: All listeners from Create Quiz Button removed!");
        }

        if (editQuizButton != null)
        {
            editQuizButton.onClick.RemoveAllListeners();
            UnityEngine.Debug.Log("QUIZ MENU ON DISABLE: All listeners from Edit Quiz Button removed!");
        }

        if (importQuizButton != null)
        {
            importQuizButton.onClick.RemoveAllListeners();
            UnityEngine.Debug.Log("QUIZ MENU ON DISABLE: All listeners from Import Quiz Button removed!");
        }

        if (backButton != null)
        {
            backButton.onClick.RemoveAllListeners();
            UnityEngine.Debug.Log("QUIZ MENU ON DISABLE: All listeners from Back Button removed!");
        }
    }

    public void LoadCreateQuizScene()
    {
        SceneManager.LoadScene("test-creation-mode");
    }

    public void LoadEditQuizScene()
    {
        UnityEngine.Debug.LogError("The edit quiz Scene does not exist in the Scenes folder!");
    }

    public void ImportQuizFile()
    {
        string targetApplication = "explorer.exe";
        string quizTemplateFolderPath = Application.persistentDataPath + "/QuizTemplates";

        UnityEngine.Debug.Log("Import Quiz File clicked - feature to be implemented");
        // This will need file browser functionality later

        // TODO: Open the File Explorer into the quiz template folder path when the respective button is pressed

        Process.Start(targetApplication, $"/select,\"" + quizTemplateFolderPath + "\"");

        UnityEngine.Debug.Log(targetApplication + " opened to file path: " + quizTemplateFolderPath);

        // Opens the File Explorer, but does not take the player to the quiz templates folder and does not let the player to select a quiz template file
    }
}
