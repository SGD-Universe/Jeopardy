using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjuster : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f); // Set volume slider value to saved volume or default to 100 (1.0)
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider.value); });
    }

    void ChangeVolume(float volume)
    {
        MusicManager.instance.SetVolume(volume); // Set the volume of the MusicManager
        PlayerPrefs.SetFloat("MusicVolume", volume); // Save the volume for future sessions
    }
}