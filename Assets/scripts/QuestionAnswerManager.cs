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


    void Start()
    {
        // Hide the answer text at the start
         answerText.gameObject.SetActive(false);
        sceneKey = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Load the last saved question count
        questionCount = PlayerPrefs.GetInt($"{sceneKey}_QuestionCount", 0);
        
        // Optionally, load and display the last question and answer
        if (questionCount > 0)
        {
            LoadLastQuestionAnswer();
        }
    }

    void Update()
    {
        // Check for space bar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RevealAnswer();
        }
    }

    public void SubmitQuestionAnswer()
    {
        // Get the text from the input fields
        question = questionInputField.text;
        answer = answerInputField.text;

        // Display the question
        questionText.text = question;
        answerText.text = answer;
        SaveQuestionAnswer(question, answer);


        // Clear the input fields
        questionInputField.text = "";
        answerInputField.text = "";
       



        // Hide the answer until space bar is pressed
        answerText.gameObject.SetActive(false);

    }

    private void RevealAnswer()
    {
        // Display the answer
        answerText.text = answer;
        answerText.gameObject.SetActive(true);

    }

    private void SaveQuestionAnswer(string question, string answer)
    {

        // Save question and answer using PlayerPrefs with the scene-specific key
        PlayerPrefs.SetString($"{sceneKey}_Question_{questionCount}", question);
        PlayerPrefs.SetString($"{sceneKey}_Answer_{questionCount}", answer);

        // Increment the question count
        questionCount++;
        PlayerPrefs.SetInt($"{sceneKey}_QuestionCount", questionCount);

        // Save changes to PlayerPrefs
        PlayerPrefs.Save();
    }
    private void LoadLastQuestionAnswer()
    {
        question = PlayerPrefs.GetString($"{sceneKey}_Question_{questionCount - 1}");
        answer = PlayerPrefs.GetString($"{sceneKey}_Answer_{questionCount - 1}");
        questionText.text = question;
        answerText.text = answer;

    }
    public void SetQuestion(string questionText)
    {
        question = questionText;
        questionInputField.text = question;
    }

}

