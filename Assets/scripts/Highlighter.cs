using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    }

    public void OnButtonClick()
    {
        button.image.color = highlightColor;
        SaveButtonState();
    }

    public void LoadButtonState()
    {
        if (IsButtonSelected())
        {
            button.image.color = highlightColor;
        }
        else
        {
            button.image.color = originalColor;
        }
    }

    public void SaveButtonState()
    {
        if (button.image.color == highlightColor)
        {
            ButtonStateManager.SetButtonSelected(gameObject.name, true);
        }
        else
        {
            ButtonStateManager.SetButtonSelected(gameObject.name, false);
        }
    }

    public void ResetButton()
    {
        button.image.color = originalColor;
        ButtonStateManager.SetButtonSelected(gameObject.name, false);
    }

    public static void ResetAllButtons()
    {
        Highlighter[] highlighters = FindObjectsOfType<Highlighter>();
        foreach (Highlighter h in highlighters)
        {
            h.ResetButton();
        }
    }

    private bool IsButtonSelected()
    {
        return ButtonStateManager.IsButtonSelected(gameObject.name);
 
    }

}

