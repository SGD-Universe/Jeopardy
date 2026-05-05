using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadQuizzesMenu : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject scrollViewContentObject; // The object that will be the parent of saved game buttons when instantiating them.

    [Header("Buttons")]
    public SavedGameButton savedGameButtonPrefab;
    public Button backButton;

    [Header("Screens")]
    [SerializeField] private GameObject mainMenuScreen;

    void Awake()
    {
        
    }

    void OnEnable()
    {


        backButton.onClick.AddListener(() => HideMenu(gameObject));
        backButton.onClick.AddListener(() => ShowMenu(mainMenuScreen));
    }

    void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HideMenu(GameObject menuObject)
    {
        menuObject.SetActive(false);
    }

    void ShowMenu(GameObject menuObject)
    {
        menuObject.SetActive(true);
    }

    void CreateSavedGameButton()
    {
        
    }
}
