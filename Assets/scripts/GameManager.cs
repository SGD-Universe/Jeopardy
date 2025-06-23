using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float teamOneScore;
    public float teamTwoScore;
    public float teamThreeScore;


    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TriggerQuestionCorrect()
    {
        AudioManager.Instance.PlaySoundCorrect();
    }

    public void TriggerQuestionIncorrect()
    {
        AudioManager.Instance.PlaySoundIncorrect();
    }
}
