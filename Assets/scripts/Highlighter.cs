using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class Highlighter : MonoBehaviour
{
    private Button button;
    private Color originalColor;
    private Color highlightColor = Color.yellow;

    void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.colors.normalColor;
        LoadButtonState();
        // sets button components and loads buttons
    }

    public void OnButtonClick()
    {
        button.image.color = highlightColor;
        SaveButtonState();
        //highlights and saves buttons on player interaction
    }

    public void LoadButtonState()
    {
        if (IsButtonSelected())
        {
            button.image.color = highlightColor;
            //highlights buttons if clicked
        }
        else
        {
            button.image.color = originalColor;
            //Returns buttons to original state after clicking
        }
    }

    public void SaveButtonState()
    {
        if (button.image.color == highlightColor)
        {
            ButtonStateManager.SetButtonSelected(gameObject.name, true);
            //saves buttons as highlighted if clicked
        }
        else
        {
            ButtonStateManager.SetButtonSelected(gameObject.name, false);
            //removes highlight from button
        }
    }

    public void ResetButton()
    {
        button.image.color = originalColor;
        ButtonStateManager.SetButtonSelected(gameObject.name, false);
        //resets button state to orignal state
    }

    public static void ResetAllButtons()
    {
        Highlighter[] highlighters = FindObjectsOfType<Highlighter>();
        foreach (Highlighter h in highlighters)
        {
            h.ResetButton();
            
        }
        //resets all buttons to original state
    }

    private bool IsButtonSelected()
    {
        return ButtonStateManager.IsButtonSelected(gameObject.name);
        // checks to see if button is selected? not 100% sure with this one
 
    }

}

