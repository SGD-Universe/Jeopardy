using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextButtons : MonoBehaviour
{
    public void OnNextButtonClick()
    {
        // Load scene 1 (assuming it's at build index 1)
        SceneManager.LoadScene(1);
    
    }
    
}
