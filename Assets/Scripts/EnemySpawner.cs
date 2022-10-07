using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    const float MIN_SPAWN_TIME = 1f;
    const float MAX_SPAWN_TIME = 10f;

    [SerializeField][Range(1f, 10f)] private float spawnTime = 5;
    [SerializeField][Range(0, 10)] private int shooterSpawnCount = 3;
    [SerializeField][Range(0, 10)] private int chaserSpawnCount = 3;

    [SerializeField] private Slider spawnTimeSlider;
    [SerializeField] private TMP_Text spawnNumberText;

    [SerializeField] private Transform spawnPoints;

    [SerializeField] private GameObject shooterPrefab;
    [SerializeField] private GameObject chaserPrefab;

    private int maxEnemyCount;

    private float timer = 0.0f;
    private bool isSpawning = false;

    private List<Enemy> enemyList = new List<Enemy>();


    private void Awake()
    {
        maxEnemyCount = 0;
        foreach(Transform child in spawnPoints)
        {
            maxEnemyCount++;
        }
    }


    private void Update()
    {
        if (isSpawning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnEnemies();
                ResetSpawnerTimer();
            }
        }
    }

    public void ChangeTime()
    {
        spawnTime = spawnTimeSlider.value * (MAX_SPAWN_TIME - MIN_SPAWN_TIME) + MIN_SPAWN_TIME;
    }
    public void ChangeNumberText()
    {
        spawnNumberText.text = spawnTime.ToString("N1");
    }

    public void DisableEnemyActions()
    {
        foreach(Enemy enemy in enemyList)
        {
            enemy.enabled = false;
        }
    }

    public void EnableEnemyActions()
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.enabled = true;
        }
    }

    public void ClearEnemies()
    {
        for(var i = enemyList.Count - 1; i >= 0; i--)
        {
            var enemy = enemyList[i];
            enemyList.RemoveAt(i);
            Destroy(enemy.gameObject.GetComponent<ShipHealth>().HealthBar.gameObject);
            Destroy(enemy.gameObject);
        }
    }

    public void StartSpawner()
    {
        isSpawning = true;
    }
    public void PauseSpawner()
    {
        isSpawning = false;
    }

    public void SpawnEnemies()
    {
        // shooters first
        var spawnedShooters = 0;
        var spawnedChasers = 0;

        var shooterDesiredProportion = 0.5f;
        if((chaserSpawnCount + shooterSpawnCount) == 0)
        {
            shooterDesiredProportion = 0.0f;
        }
        else
        {
            shooterDesiredProportion = (float) shooterSpawnCount / (float) (chaserSpawnCount + shooterSpawnCount);
        }

        var shootersOnScene = 0;
        var chasersOnScene = 0;
        var shooterOnSceneProportion = 0.5f;

        foreach(Enemy enemy in enemyList)
        {
            if(enemy is Chaser)
            {
                chasersOnScene++;
            }
            if (enemy is Shooter)
            {
                shootersOnScene++;
            }
        }

        if ((chasersOnScene + shootersOnScene) == 0)
        {
            shooterOnSceneProportion = 0.0f;
        }
        else
        {
            shooterOnSceneProportion = (float)shootersOnScene / (float)(chasersOnScene + shootersOnScene);
        }

        if(shooterOnSceneProportion > shooterDesiredProportion)
        {
            spawnedShooters = shooterSpawnCount;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            // will spawn if spawnpoint is 'free' (no colliders around radius of 2)
            if (Physics2D.OverlapCircleAll(spawnPoint.position, 2).Length == 0)
            {
                if(spawnedShooters < shooterSpawnCount)
                {
                    var shooter = Instantiate(shooterPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
                    enemyList.Add(shooter);
                    spawnedShooters++;
                }
                else if (spawnedChasers < chaserSpawnCount)
                {
                    var chaser = Instantiate(chaserPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
                    enemyList.Add(chaser);
                    spawnedChasers++;
                }
            }
        }
    }

    public void ResetSpawnerTimer()
    {
        timer = spawnTime;
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }
}
