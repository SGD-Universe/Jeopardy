using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera menuCamera;      // Previously virtualCamera
    [SerializeField] private CinemachineVirtualCamera gameCamera;      // Previously virtualCamera2

    [Header("Look Targets")]
    [SerializeField] private GameObject currentLookAt;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject gameScreen;

    private void Start()
    {
        // Basic safety checks
        if (menuCamera == null)
            Debug.LogError("[CameraManager] Menu camera is not assigned.");
        if (gameCamera == null)
            Debug.LogError("[CameraManager] Game camera is not assigned.");
        if (menuScreen == null)
            Debug.LogWarning("[CameraManager] Menu screen object is not assigned.");
        if (gameScreen == null)
            Debug.LogWarning("[CameraManager] Game screen object is not assigned.");

        // Default to menu on start if nothing is assigned
        if (currentLookAt == null && menuScreen != null)
            currentLookAt = menuScreen;

        // Start on menu camera
        SetActiveCamera(isGame: false);
    }

    private void Update()
    {
        // Always keep the menu camera looking at the current target (if set)
        if (menuCamera != null && currentLookAt != null)
        {
            menuCamera.LookAt = currentLookAt.transform;
            menuCamera.Follow = currentLookAt.transform;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PerformTransitionGoBack();
        }
    }

    /// <summary>
    /// Called from a UI Button (e.g., Start Game).
    /// </summary>
    public void PerformTransitionToGameScreen()
    {
        if (gameScreen != null)
            currentLookAt = gameScreen;

        SetActiveCamera(isGame: true);

        // Optional: toggle screen visibility
        if (menuScreen != null) menuScreen.SetActive(false);
        if (gameScreen != null) gameScreen.SetActive(true);

        // If you have a game start method, call it here:
        // ExperimentalGameScreen.Instance.BeginGame();
    }

    /// <summary>
    /// Return to the menu from the game (Escape key or UI button).
    /// </summary>
    public void PerformTransitionGoBack()
    {
        if (menuScreen != null)
            currentLookAt = menuScreen;

        SetActiveCamera(isGame: false);

        // Optional: toggle screen visibility
        if (menuScreen != null) menuScreen.SetActive(true);
        if (gameScreen != null) gameScreen.SetActive(false);
    }

    /// <summary>
    /// Handles Cinemachine priorities so only one camera is active.
    /// </summary>
    private void SetActiveCamera(bool isGame)
    {
        if (menuCamera == null || gameCamera == null)
            return;

        if (isGame)
        {
            menuCamera.Priority = 0;
            gameCamera.Priority = 1;
        }
        else
        {
            menuCamera.Priority = 1;
            gameCamera.Priority = 0;
        }
    }
}
