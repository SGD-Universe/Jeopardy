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
        virtualCamera.LookAt = currentLookAt.transform;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MakeTransitionGoback();
            //Camera.main.GetComponent<Animator>().Play("CameraPivotFromScreenToMenu");
        }
    }

    //called from gameobject: CreateNewQuiz: Button
    public void MakeTransitionToGameScreen()
    {
        currentLookAt = gameScreen;

        //Moves camera
        //Camera.main.GetComponent<Animator>().enabled = true;
        //Camera.main.GetComponent<Animator>().Play("CameraPivotFromMenuToScreen");
        //ExperimentalGameScreen.Instance.BeginGame();
    }

    public void MakeTransitionGoback()
    {
        currentLookAt = menuScreen;
    }
}
