using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField][Range(60f, 180f)] private float sessionTimeInSeconds = 60;
    [SerializeField] private Slider sessionTimeSlider;
    [SerializeField] private TMP_Text sessionTimeText;


    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        audioManager.PlayMusic("Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
