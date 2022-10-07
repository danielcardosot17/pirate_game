using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy, IEnemyAI
{
    [SerializeField][Range(1f, 10f)] private float followRadius = 10f;
    [SerializeField][Range(1f, 5f)] private float explosionRadius = 3f;

    [SerializeField][Range(0.5f, 10f)] private float moveSpeed = 7;
    [SerializeField][Range(10f, 100f)] private float rotationSpeed = 20;

    [SerializeField][Range(1, 10)] private int damage = 5;

    private AudioManager audioManager;
    private Transform player;
    private Vector3 distanceToPlayer;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    public void Attack()
    {
        if(distanceToPlayer.magnitude <= explosionRadius)
        {
            Explode();
        }
    }

    private void Explode()
    {

    }

    public void Move()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
    }

    private void Update()
    {
        distanceToPlayer = (player.position - transform.position);
        Move();
        Attack();
    }
}
