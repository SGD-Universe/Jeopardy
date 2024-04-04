using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    void Start()
    {
        optionsMenu.SetActive(false); // Hide the options menu initially
    }

    public void ToggleOptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf); // Toggle the active state of the options menu
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false); // Close the options menu
    }
}
