using UnityEngine;
using UnityEngine.SceneManagement;

//Attached on GameObject: GameManager

//This script is currently used to move from one scene to another with LoadSceneByName(string sceneName)
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    // loads the scene by name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // note: this will only works in game, not in editor
    public void QuitGame()
    {
        Application.Quit();
    }
}
