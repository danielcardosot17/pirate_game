using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    const float MIN_SESSION_TIME = 60f;
    const float MAX_SESSION_TIME = 180f;

    [SerializeField][Range(60f, 180f)] private float sessionTimeInSeconds = 60;

    [SerializeField] private Slider sessionTimeSlider;
    [SerializeField] private TMP_Text sessionNumberText;


    private SessionTimer sessionTimer;
    private AudioManager audioManager;
    private MenuManager menuManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        menuManager = FindObjectOfType<MenuManager>();
        sessionTimer = FindObjectOfType<SessionTimer>();
    }
    void Start()
    {
        audioManager.PlayMusic("Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        sessionTimer.SetTimerLength(sessionTimeInSeconds);
        sessionTimer.StartTimer();
    }

    public void ChangeTime()
    {
        sessionTimeInSeconds = sessionTimeSlider.value * (MAX_SESSION_TIME - MIN_SESSION_TIME) + MIN_SESSION_TIME;
    }
    public void ChangeNumberText()
    {
        sessionNumberText.text = (sessionTimeInSeconds/60f).ToString("N1");
    }

    public void EndGame()
    {

    }
}
