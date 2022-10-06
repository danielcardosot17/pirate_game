using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionTimer : MonoBehaviour
{
    const float MIN_SESSION_TIME = 60f;
    const float MAX_SESSION_TIME = 180f;

    [SerializeField][Range(60f, 180f)] private float sessionTimeInSeconds = 60;

    [SerializeField] private Slider sessionTimeSlider;
    [SerializeField] private TMP_Text sessionNumberText;
    [SerializeField] private TMP_Text timerText;
    
    private GameMaster gameMaster;
    private bool isTimer = false;
    private float timerLength = 0.0f;

    public float SessionTimeInSeconds { get => sessionTimeInSeconds; }

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            DisplayTime();
            timerLength -= Time.deltaTime;
            if (timerLength <= 0)
            {
                timerLength = 0.0f;
                PauseTimer();
                gameMaster.EndGame();
            }
        }
    }
    private void DisplayTime()
    {
        timerText.text = TimeSpan.FromSeconds(timerLength).ToString("mm\\:ss");
    }
    public void StartTimer()
    {
        isTimer = true;
    }

    public void PauseTimer()
    {
        isTimer = false;
    }
    public void SetTimerLength(float length)
    {
        timerLength = length;
    }

    public void ChangeTime()
    {
        sessionTimeInSeconds = sessionTimeSlider.value * (MAX_SESSION_TIME - MIN_SESSION_TIME) + MIN_SESSION_TIME;
    }
    public void ChangeNumberText()
    {
        sessionNumberText.text = (sessionTimeInSeconds / 60f).ToString("N1");
    }
}
