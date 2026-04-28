using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SavedGameButton : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button savedGameButton;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI quizNameText;
    [SerializeField] private TextMeshProUGUI teamScoreText;

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

    }
}
