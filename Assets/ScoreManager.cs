using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int Score;
    public TextMeshProUGUI scoreText;
    private void Start()
    {
        AddScore(0);
    }
    public void AddScore(int score)
    {
        Score += score;
        scoreText.text = "Score: " + Score.ToString();
    }
}
