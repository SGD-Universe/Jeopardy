using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuestionAnswerManager : MonoBehaviour
{
    public TMP_InputField questionInputField;
    public TMP_InputField answerInputField;
    public TMP_Text questionText;
    public TMP_Text answerText;

    private string question;
    private string answer;

    private int questionCount = 0;
    private string sceneKey;

    public int whichQuestion;

    // This indicates when a QA manager is in edit mode
    public bool isCreating = false;


    void Start()
    {
        // Hide the answer text at the start
        if (isCreating == false)
        {
            answerText.gameObject.SetActive(false);
        }
        sceneKey = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Load the last saved question count
        questionCount = PlayerPrefs.GetInt($"{whichQuestion}_QuestionCount", 0);
        
        // Optionally, load and display the last question and answer
        if (questionCount > 0)
        {
            LoadLastQuestionAnswer();
        }
    }

    void Update()
    {

    }

    public void RevealAnswer()
    {
        if (isCreating == false)
        {
            answerText.text = answer;
            answerText.gameObject.SetActive(true);
        }
    }


    public void SubmitQuestionAnswer()
    {
        // Get the text from the input fields
        question = questionInputField.text;
        answer = answerInputField.text;

        // Saves the stuff
        SaveQuestionAnswer(question, answer);
    }

    private void SaveQuestionAnswer(string question, string answer)
    {
        // Save question and answer using PlayerPrefs with whichQuestion. If input is blank, uses the previous saved data.
        if (questionInputField.text == "")
            question = PlayerPrefs.GetString($"{whichQuestion}_Question_{questionCount - 1}");
        PlayerPrefs.SetString($"{whichQuestion}_Question_{questionCount}", question);

        if (answerInputField.text == "")
            answer = PlayerPrefs.GetString($"{whichQuestion}_Answer_{questionCount - 1}");
        PlayerPrefs.SetString($"{whichQuestion}_Answer_{questionCount}", answer);

        // Increment the question count. I'm not entirely sure what that does, it's legacy code. Things break if I turn it off so I'm not touching it.
        questionCount++;
        PlayerPrefs.SetInt($"{whichQuestion}_QuestionCount", questionCount);

        // Save changes to PlayerPrefs
        PlayerPrefs.Save();
    }

    private void LoadLastQuestionAnswer()
    {
        // If/else statements make placeholder text happen if needed
        if (string.IsNullOrEmpty(PlayerPrefs.GetString($"{whichQuestion}_Question_{questionCount - 1}")))
        {
            questionText.text = "Enter text here";
        }
        else
        {
            question = PlayerPrefs.GetString($"{whichQuestion}_Question_{questionCount - 1}");
            questionText.text = question;
        }

        if (string.IsNullOrEmpty(PlayerPrefs.GetString($"{whichQuestion}_Answer_{questionCount - 1}")))
        {
            answerText.text = "Enter text here";
        }
        else
        {
            answer = PlayerPrefs.GetString($"{whichQuestion}_Answer_{questionCount - 1}");
            answerText.text = answer;
        }

    }

    public void SetQuestion(string questionText)
    {
        // This might be depreciated, not entirely sure
        question = questionText;
        questionInputField.text = question;
    }
}

