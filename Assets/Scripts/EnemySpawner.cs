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
    [SerializeField][Range(1, 10)] private int shooterSpawnCount = 3;
    [SerializeField][Range(1, 10)] private int chaserSpawnCount = 3;

    [SerializeField] private Slider spawnTimeSlider;
    [SerializeField] private TMP_Text spawnNumberText;






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

    }

    public void EnableEnemyActions()
    {

    }
}
