using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalGameManager : MonoBehaviour
{
    public static ExperimentalGameManager Instance;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.main.GetComponent<Animator>().Play("CameraPivotFromScreenToMenu");
        }
    }

    public void TransitionToGameScreen()
    {
        Camera.main.GetComponent<Animator>().enabled = true;
        Camera.main.GetComponent<Animator>().Play("CameraPivotFromMenuToScreen");
        ExperimentalGameScreen.Instance.BeginGame();
    }
}
