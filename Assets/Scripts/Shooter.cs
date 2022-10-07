using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : Enemy, IEnemyAI
{

    [SerializeField][Range(1f, 10f)] private float actionRadius = 5f;
    [SerializeField][Range(0.1f, 3f)] private float rotationSpeed = 1f;

    [SerializeField][Range(1, 10)] private int frontalDamage = 5;

    [SerializeField][Range(1f, 10f)] private float bulletDistance = 5f;
    [SerializeField][Range(0.1f, 2f)] private float bulletTime = 0.5f;
    [SerializeField][Range(0.5f, 3f)] private float attackReloadTime = 1f;


    [SerializeField] private Transform frontalSpawnPoint;
    [SerializeField] private GameObject frontalBullet;

    private AudioManager audioManager;
    private Transform player;
    private Vector3 distanceToPlayer;
    private float timer = 0.0f;
    private bool isReady = true;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerMovement>().transform;
        timer = attackReloadTime;
    }

    public void Attack()
    {
        if(isReady)
        {
            if(distanceToPlayer.magnitude <= actionRadius)
            {
                Shoot();
                audioManager.PlaySFX("Attack");
                isReady = false;
                timer = attackReloadTime;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0.0f;
                isReady = true;
            }
        }
    }

    private void Shoot()
    {
        var frontalCannonBall = Instantiate(frontalBullet, frontalSpawnPoint.position, frontalSpawnPoint.rotation);
        var bullet = frontalCannonBall.GetComponent<Bullet>();
        bullet.SetDamage(frontalDamage);
        bullet.Move(transform.up, bulletDistance, bulletTime);
    }

    public void Move()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        transform.up = Vector3.RotateTowards(transform.up, distanceToPlayer.normalized, rotationSpeed * Time.deltaTime, 0.0f);
    }

    private void Update()
    {
        distanceToPlayer = (player.position - transform.position);
        Move();
        Attack();
    }

}
