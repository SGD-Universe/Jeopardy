using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameTemp : MonoBehaviour
{
    public static GameManager Instance;
    public Button playButton;

    public void PlayGame()
    {
        playButton.onClick.AddListener(() => GameManager.Instance.LoadSceneByName("PlayGame"));
    }
}
