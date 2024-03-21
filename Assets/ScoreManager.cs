using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int Score;
    public static int TotalGlissNodes;

    public TextMeshProUGUI scoreText;
    private void Start()
    {
        AddScore(0);
        TotalGlissNodes = 0;
    }
    public void AddScore(int score)
    {
        Score += score;
        scoreText.text = "Score: " + Score.ToString();
    }

    public void incrementGlissNodes()
    {
        if (TotalGlissNodes < 3)
            TotalGlissNodes++;
        Debug.Log("TotalGlissNodes : " + TotalGlissNodes);
    }
}
