using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //This script may be unnecessary, as Team Selection does not actually exist in the scenes listed in the game
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("TeamSelection").name);
        //Calls on loading the TeamSelection scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
        //Prints "Quit" to the console and kills the application
    }


}
