using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("TeamSelection").name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
