using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public string teamName;
    public int score;

    public Team(string name)
    {
        teamName = name;
        score = 0;
    }
}
