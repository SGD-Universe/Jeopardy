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
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.volume = 1.0f; // Set volume to 100 (1.0)
            audioSource.Play(); // Start playing the music
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}