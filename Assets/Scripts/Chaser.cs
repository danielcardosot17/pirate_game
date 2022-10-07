using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy, IEnemyAI
{
    [SerializeField][Range(1f, 10f)] private float followRadius = 10f;
    [SerializeField][Range(0.1f, 3f)] private float explosionRadius = 1f;

    [SerializeField][Range(0.5f, 10f)] private float moveSpeed = 5;
    [SerializeField][Range(0.1f, 3f)] private float rotationSpeed = 1f;

    [SerializeField][Range(1, 10)] private int damage = 5;
    [SerializeField] private GameObject explosionPs;

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
        DamageShipsAround();
        audioManager.PlaySFX("MissOrHit");
        Instantiate(explosionPs, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void DamageShipsAround()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(var collider in colliders)
        {
            if(collider.gameObject.GetComponent<ShipHealth>() != null)
            {
                var shipHealth = collider.gameObject.GetComponent<ShipHealth>();
                shipHealth.TakeDamage(damage);
            }
        }
    }

    public void Move()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        LookAtPlayer();
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        var forwardDirectionMagnitude = Vector3.Project(transform.up, distanceToPlayer).magnitude;
        transform.position += moveSpeed * Time.deltaTime * forwardDirectionMagnitude * transform.up;
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
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
