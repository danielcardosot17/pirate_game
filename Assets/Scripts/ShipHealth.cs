using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField][Range(1,100)] private int initialHealth = 10;
    [SerializeField] private GameObject explosionPs;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Sprite[] shipDeteriorationSprites;

    private int health;
    private Transform healthBar;
    private Transform fill;
    private Vector3 offsetSprite;

    private PlayerScoreManager playerScoreManager;
    private GameMaster gameMaster;
    private EnemySpawner enemySpawner;
    private SpriteRenderer spriteRenderer;

    public Transform HealthBar { get => healthBar; }

    private void Awake()
    {
        playerScoreManager = FindObjectOfType<PlayerScoreManager>();
        gameMaster = FindObjectOfType<GameMaster>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        spriteRenderer = gameObject.transform.Find("Graphics").Find("Ship").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        health = initialHealth;
        healthBar = Instantiate(healthBarPrefab, transform.position + offset, Quaternion.identity).GetComponent<Transform>();
        offsetSprite = healthBar.GetComponentInChildren<SpriteRenderer>().bounds.extents;
        fill = healthBar.Find("Background").Find("Fill");
    }

    private void LateUpdate()
    {
        HealthBarFollow();
    }

    private void HealthBarFollow()
    {
        healthBar.position = transform.position + offset - offsetSprite;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        var localScale = fill.localScale;
        if(health <= 0)
        {
            localScale.x = 0;
            fill.localScale = localScale;
            health = 0;
            Die();
        }
        else 
        {
            localScale.x = (float)health/ (float)initialHealth;
            fill.localScale = localScale;
            DeteriorateSprite();
        }
    }

    private void DeteriorateSprite()
    {
        var percentage = (float)health / (float)initialHealth;
        if (percentage <= 0.3f)
        {
            spriteRenderer.sprite = shipDeteriorationSprites[2];
        }
        else if (percentage <= 0.6f)
        {
            spriteRenderer.sprite = shipDeteriorationSprites[1];
        }
    }

    private void ResetSprite()
    {
        spriteRenderer.sprite = shipDeteriorationSprites[0];
    }


    public void ResetHP()
    {
        health = initialHealth;
        var localScale = fill.localScale;
        localScale.x = 1;
        fill.localScale = localScale;
        ResetSprite();
    }

    private void Die()
    {
        if(gameObject.GetComponent<PlayerMovement>() != null) // is Player
        {
            gameMaster.EndGame();
        }
        else if (gameObject.GetComponent<Enemy>() != null) // is Enemy
        {
            playerScoreManager.IncreaseScore();
            playerScoreManager.DisplayPlayerScore();
            enemySpawner.RemoveEnemyFromList(gameObject.GetComponent<Enemy>());
            Instantiate(explosionPs, transform.position, Quaternion.identity);
            Destroy(healthBar.gameObject);
            Destroy(gameObject);
        }
    }
}
