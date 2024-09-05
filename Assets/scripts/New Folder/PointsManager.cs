using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public Text pointsText;
    private int points = 0;

    // Pretty sure this script is not utilized
    void Start()
    {
        UpdatePointsText();
        //calls on points update on startup
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsText();
        //adds points
    }

    public void SubtractPoints(int amount)
    {
        points -= amount;
        UpdatePointsText();
        //subtracts points
    }

    void UpdatePointsText()
    {
        pointsText.text = "" + points.ToString();
        //sets up how points are updated
    }
}