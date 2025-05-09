using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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


}
