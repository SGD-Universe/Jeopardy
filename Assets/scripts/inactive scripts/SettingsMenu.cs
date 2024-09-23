using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Reference to the options menu panel
    public GameObject settingsMenu;

    // Reference to the volume slider and volume button UI elements
    public GameObject volumeSlider;
    public Button volumeButton;

    // Reference to the close button
    public Button backButton;

    void Start()
    {
        // Ensure the options panel and volume slider start inactive
        settingsMenu.SetActive(false);
        volumeSlider.SetActive(false);

        // Set up button listeners
        volumeButton.onClick.AddListener(ToggleVolumeSlider);
        backButton.onClick.AddListener(CloseOptionsMenu);
    }

    // Function to open the Settings Menu panel
    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    // Toggle the volume slider's visibility
    public void ToggleVolumeSlider()
    {
        volumeSlider.SetActive(!volumeSlider.activeSelf);
    }

    // Close the Options Menu panel
    public void CloseOptionsMenu()
    {
        settingsMenu.SetActive(false);
    }

    // Function to load the Main Menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Called when the volume slider's value changes
    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
