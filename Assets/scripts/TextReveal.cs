using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TextReveal : MonoBehaviour
{
    // Reference to the UI Text component
    public Text textComponent;

    // The text to be revealed
    public string revealedText = "Your answer or text here";

    // The original text before revealing
    private string originalText;

    // Track whether the text is revealed
    private bool isTextRevealed = false;

    void Start()
    {
        // Save the original text
        originalText = textComponent.text;
    }

    void Update()
    {
        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle text visibility
            isTextRevealed = !isTextRevealed;

            // Update text component
            if (isTextRevealed)
            {
                textComponent.text = revealedText;
            }
            else
            {
                textComponent.text = originalText;
            }
        }
    }
}
