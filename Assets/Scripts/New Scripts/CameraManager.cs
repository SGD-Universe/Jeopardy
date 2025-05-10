using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Used to refrence the virtualCamera used to guide the camera
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    //Current GameObject virtualCamera is looking at
    [SerializeField] private GameObject currentLookAt;

    //Game Menu screen
    [SerializeField] private GameObject menuScreen;

    //game board Screen
    [SerializeField] private GameObject gameScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Camera will look at whichever object is made currentLookAt
        virtualCamera.LookAt = currentLookAt.transform;


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

        //ExperimentalGameScreen.Instance.BeginGame();
    }

    //Currently really rough, will jump back to menuScreen right now, should be able 
    public void PerformTransitionGoback()
    {
        currentLookAt = menuScreen;
    }
}
