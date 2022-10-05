using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Range(0.5f, 10f)] private float spawnTime = 5;
    [SerializeField][Range(1, 10)] private int shooterSpawnCount = 3;
    [SerializeField][Range(1, 10)] private int chaserSpawnCount = 3;
}
