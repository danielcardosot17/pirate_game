using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SessionTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    
    private GameMaster gameMaster;
    private bool isTimer = false;
    private float timerLength = 0.0f;
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
}
