using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreRealtimeText;

    private int score = 0;

    public void ResetScore()
    {
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void DisplayPlayerScore()
    {
        scoreText.text = score.ToString("N0");
        scoreRealtimeText.text = score.ToString("N0");
    }
}
