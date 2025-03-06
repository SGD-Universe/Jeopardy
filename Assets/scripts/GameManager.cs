using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Dictionary to track answered questions by category
    public Dictionary<string, List<string>> answeredQuestionsByCategory = new Dictionary<string, List<string>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Loads the scene by name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
  public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting..."); // Just to confirm in the editor
    }
    // Saves an answered question
    public void AddAnsweredQuestion(string category, string questionText)
    {
        if (!answeredQuestionsByCategory.ContainsKey(category))
        {
            answeredQuestionsByCategory[category] = new List<string>();
        }
        answeredQuestionsByCategory[category].Add(questionText);
    }
}