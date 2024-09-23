using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public GameObject volumeSlider; // Reference to the volume slider GameObject

    public void ToggleVolumeSlider()
    {
        volumeSlider.SetActive(!volumeSlider.activeSelf); // Toggle the visibility of the volume slider
    }
}
