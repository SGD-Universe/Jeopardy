using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public static VolumeControl instance; // Static reference to the VolumeControl instance
    public Slider volumeSlider; // Reference to the volume slider

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the VolumeControl when loading a new scene
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate VolumeControl instances
        }
    }

    void Start()
    {
        volumeSlider.gameObject.SetActive(false); // Hide the slider initially
    }

    public void ChangeVolume()
    {
        if (volumeSlider != null)
        {
            // Change the volume of the audio source
            // For example, if you have a reference to an audio source:
            // audioSource.volume = volumeSlider.value;
        }
    }

    public void ToggleSliderVisibility()
    {
        if (volumeSlider != null)
        {
            volumeSlider.gameObject.SetActive(!volumeSlider.gameObject.activeSelf);
        }
    }
}