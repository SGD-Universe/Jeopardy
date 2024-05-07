using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("Team selection").name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
