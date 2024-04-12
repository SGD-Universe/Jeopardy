using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour
{
    public Text teamNameText;

    public void Initialize(string name)
    {
        teamNameText.text = name;
    }
}