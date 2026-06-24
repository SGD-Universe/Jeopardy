using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class LoadQuizzesMenu : MonoBehaviour
{
    public ScrollRect sR;
    public RectTransform content;

    [Header("Quiz List Setup")]
    public GameObject quizButtonPrefab; // A button prefab representing a single quiz item

    [Header("Camera")]
    public CameraManager cameraManager; // Drag the CameraManager from the scene

    void Start()
    {
        // Populate the list when the menu starts up
        PopulateQuizList();
    }

    public void PopulateQuizList()
    {
        // 1. Clear out existing items in the Scroll View's Content to prevent duplication
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 2. Locate the folder where the quizzes are saved
        string quizTemplateFolderPath = Application.streamingAssetsPath + "/QuizTemplates";
        if (!Directory.Exists(quizTemplateFolderPath))
        {
            Directory.CreateDirectory(quizTemplateFolderPath);
        }

        // 3. Find all .json quiz templates in that folder
        string[] quizFiles = Directory.GetFiles(quizTemplateFolderPath, "*.json");

        // 4. Instantiate a button for each quiz file
        foreach (string filePath in quizFiles)
        {
            string quizName = Path.GetFileNameWithoutExtension(filePath);

            // Instantiate the prefab inside the ScrollRect's Content transform
            GameObject newButton = Instantiate(quizButtonPrefab, content);

            // Set the button's text to show the quiz name
            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = quizName;
            }
            else
            {
                // Fallback for standard UI text
                Text standardText = newButton.GetComponentInChildren<Text>();
                if (standardText != null)
                {
                    standardText.text = quizName;
                }
            }

            // Attach a listener to select this quiz when the button is clicked
            Button btn = newButton.GetComponent<Button>();
            if (btn != null)
            {
                // Capture filePath in a local variable for the lambda closure
                string targetPath = filePath;
                btn.onClick.AddListener(() => OnQuizSelected(targetPath));
            }
        }
    }

    void OnQuizSelected(string quizFilePath)
    {
        Debug.Log("Selected Quiz Path: " + quizFilePath);
        
        // 5. Pass the path of the selected quiz to your LoadQuiz/Game manager.
        // For example:
        LoadQuiz loadQuiz = FindAnyObjectByType<LoadQuiz>();
        if (loadQuiz != null)
        {
            loadQuiz.importFilePath = quizFilePath;
            loadQuiz.importQuizName = Path.GetFileNameWithoutExtension(quizFilePath);
            loadQuiz.fileImported = true;
            
            // Trigger loading operations or proceed to the game
            loadQuiz.LoadSavedQuiz();

            // Transition the camera to the game board
            if (cameraManager != null)
            {
                cameraManager.PerformTransitionToGameScreen();
            }
            else
            {
                Debug.LogWarning("CameraManager reference is missing on LoadQuizzesMenu!");
            }
        }
    }
}
