using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //not sure what this does
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1.0f); // Set volume to saved volume or 100 if not applicable
            audioSource.Play(); // Start playing the music
        }
        //starts playing music on startup
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    //sets vulume level
}