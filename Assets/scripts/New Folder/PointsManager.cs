using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public Text pointsText;
    private int points = 0;

    void Start()
    {
        UpdatePointsText();
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsText();
    }

    public void SubtractPoints(int amount)
    {
        points -= amount;
        UpdatePointsText();
    }

    void UpdatePointsText()
    {
        pointsText.text = "" + points.ToString();
    }
}