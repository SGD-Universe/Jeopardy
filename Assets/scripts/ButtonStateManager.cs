using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStateManager : MonoBehaviour
{
    private static HashSet<string> selectedButtons = new HashSet<string>();

    public static bool IsButtonSelected(string buttonName)
    {
        return selectedButtons.Contains(buttonName);
    }

    public static void SetButtonSelected(string buttonName, bool selected)
    {
        if (selected)
        {
            selectedButtons.Add(buttonName);
        }
        else
        {
            selectedButtons.Remove(buttonName);
        }
    }
}