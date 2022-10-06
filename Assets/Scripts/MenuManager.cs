using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startgameCanvas;
    [SerializeField] private GameObject endgameCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject duringGameCanvas;


    private void Awake()
    {
        HideStartgameCanvas();
        HideEndgameCanvas();
        HideOptionsCanvas();
        HidePauseCanvas();
        HideDuringGameCanvas();
    }

    public void HidePauseCanvas()
    {
        pauseCanvas.SetActive(false);
    }
    public void HideDuringGameCanvas()
    {
        duringGameCanvas.SetActive(false);
    }

    public void HideOptionsCanvas()
    {
        optionsCanvas.SetActive(false);
    }

    public void HideEndgameCanvas()
    {
        endgameCanvas.SetActive(false);
    }

    public void HideStartgameCanvas()
    {
        startgameCanvas.SetActive(false);
    }

    public void ShowEndgameCanvas()
    {
        endgameCanvas.SetActive(true);
    }

    public void ShowStartgameCanvas()
    {
        startgameCanvas.SetActive(true);
    }

    public void ShowPauseCanvas()
    {
        pauseCanvas.SetActive(true);
    }

    public void ShowOptionsCanvas()
    {
        optionsCanvas.SetActive(true);
    }

    public void ShowDuringGameCanvas()
    {
        duringGameCanvas.SetActive(true);
    }

}
