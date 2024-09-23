using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


}