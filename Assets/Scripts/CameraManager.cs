using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Used to refrence the virtualCamera used to guide the camera
    [SerializeField] private CinemachineVirtualCamera mainMenuVC;

    // Second camera looking at game board
    [SerializeField] private CinemachineVirtualCamera gameScreenVC;

    // Third camera looking at contestants
    [SerializeField] private CinemachineVirtualCamera contestantsVC;

    //Current GameObject virtualCamera is looking at
    [SerializeField] private GameObject currentLookAt;

    //Game Menu screen
    [SerializeField] private GameObject menuScreen;

    //game board Screen
    [SerializeField] private GameObject gameScreen;

    //contestants Screen
    [SerializeField] private GameObject contestantsScreen;

    void Awake()
    {
        mainMenuVC.Priority = 1;
        gameScreenVC.Priority = 0;
        contestantsVC.Priority = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Camera will look at whichever object is made currentLookAt
        mainMenuVC.LookAt = currentLookAt.transform;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PerformTransitionGoback();
        }
    }

    //called from gameobject: CreateNewQuiz: Button
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

    //Currently really rough, will jump back to menuScreen right now, should be able 
    public void PerformTransitionGoback()
    {
        currentLookAt = menuScreen;
        mainMenuVC.Priority = 1;
        gameScreenVC.Priority = 0;
        contestantsVC.Priority = 0;
    }
}
