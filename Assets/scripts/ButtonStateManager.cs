using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStateManager : MonoBehaviour
{
    private static HashSet<string> selectedButtons = new HashSet<string>();

    public static bool IsButtonSelected(string buttonName)
    {
        return selectedButtons.Contains(buttonName);
        //determines if button is selected
    }

    public static void SetButtonSelected(string buttonName, bool selected)
    {
        if (selected)
        {
            selectedButtons.Add(buttonName);
            //adds selected button to command prompts
        }
        else
        {
            selectedButtons.Remove(buttonName);
            //remoces selected buttons from command prompts
        }
    }
}