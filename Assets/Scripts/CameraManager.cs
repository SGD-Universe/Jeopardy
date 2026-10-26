using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Used to reference the virtualCamera used to guide the camera
    [SerializeField] private CinemachineVirtualCamera mainMenuVC;

    // Second camera looking at game board
    [SerializeField] private CinemachineVirtualCamera gameScreenVC;

    // Third camera looking at contestants
    [SerializeField] private CinemachineVirtualCamera contestantsVC;

    // Current GameObject virtualCamera is looking at
    [SerializeField] private GameObject currentLookAt;

    // Game Menu screen
    [SerializeField] private GameObject menuScreen;

    // Game Board Screen
    [SerializeField] private GameObject gameScreen;

    // Contestants Screen
    [SerializeField] private GameObject contestantsScreen;

    void Awake()
    {
        mainMenuVC.Priority = 1;
        gameScreenVC.Priority = 0;
        contestantsVC.Priority = 0;
    }

    void Update()
    {
        // Camera will look at whichever object is made currentLookAt
        mainMenuVC.LookAt = currentLookAt.transform;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PerformTransitionGoBack();
        }
        /* Doesn't work for:
        ContestantsVC (at all)
        GameScreenVC (Dollies away but doesn't look back at main)
        */
    }

    // Called from GameObject: CreateNewQuiz: Button
    public void PerformTransitionToGameScreen()
    {
        //Camera will look at the game screen now.
        currentLookAt = gameScreen;
        mainMenuVC.Priority = 0;
        gameScreenVC.Priority = 1;
        contestantsVC.Priority = 0;
    }

    // Transition to contestants view
    public void PerformTransitionToContestants()
    {
        currentLookAt = contestantsScreen;
        mainMenuVC.Priority = 0;
        gameScreenVC.Priority = 0;
        contestantsVC.Priority = 1;
    }

    // Currently really rough, will jump back to menuScreen right now, should be able... Comment cuts off here
    public void PerformTransitionGoBack()
    {
        currentLookAt = menuScreen;
        mainMenuVC.Priority = 1;
        gameScreenVC.Priority = 0;
        contestantsVC.Priority = 0;
    }
}