using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SavedGameButton : MonoBehaviour
{
    // When this button gets instantiated every time a saved game file is found in the Saved Games folder, tie the appropriate game data to each button.

    [Header("Button")]
    [SerializeField] private Button savedGameButton;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI quizNameText; // The name of the quiz template whose data is tied to the button.
    [SerializeField] private TextMeshProUGUI teamScoreText; // 

    void Awake()
    {
        
    }

    void OnEnable()
    {
        savedGameButton.onClick.AddListener(LoadSelectedGame);
    }

    void OnDisable()
    {
        savedGameButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LoadSelectedGame()
    {
        // Code for loading the game tied to a saved game button goes here.
    }
}
