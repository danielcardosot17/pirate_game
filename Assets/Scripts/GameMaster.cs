using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private SessionTimer sessionTimer;
    private AudioManager audioManager;
    private MenuManager menuManager;
    private EnemySpawner enemySpawner;
    private PlayerScoreManager playerScoreManager;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        menuManager = FindObjectOfType<MenuManager>();
        sessionTimer = FindObjectOfType<SessionTimer>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        playerScoreManager = FindObjectOfType<PlayerScoreManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }
    void Start()
    {
        audioManager.PlayMusic("Music");
        PauseGame();
        menuManager.ShowStartgameCanvas();
    }

    public void StartGame()
    {
        sessionTimer.SetTimerLength(sessionTimer.SessionTimeInSeconds);
        sessionTimer.StartTimer();
        playerScoreManager.ResetScore();
        playerScoreManager.DisplayPlayerScore();

        ResetPlayerPosition();
        ResetPlayerHP();


        enemySpawner.ResetSpawnerTimer();
        enemySpawner.StartSpawner();
        enemySpawner.SpawnEnemies();

        UnPauseGame();
    }

    private void ResetPlayerHP()
    {
        playerMovement.gameObject.GetComponent<ShipHealth>().ResetHP();
    }

    private void ResetPlayerPosition()
    {
        playerMovement.transform.position = Vector3.zero;
    }

    public void EndGame()
    {
        PauseGame();

        enemySpawner.ClearEnemies();

        menuManager.ShowEndgameCanvas();

        playerScoreManager.DisplayPlayerScore();
    }

    public void PauseGame()
    {
        playerMovement.DisablePlayerMovement();
        playerAttack.DisablePlayerAttack();
        enemySpawner.DisableEnemyActions();
        enemySpawner.PauseSpawner();
        sessionTimer.PauseTimer();
    }
    public void UnPauseGame()
    {
        playerMovement.EnablePlayerMovement();
        playerAttack.EnablePlayerAttack();
        enemySpawner.EnableEnemyActions();
        sessionTimer.StartTimer();
        enemySpawner.StartSpawner();
    }

}
