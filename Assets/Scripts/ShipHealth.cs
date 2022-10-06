using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField][Range(5f,100f)] private float health = 10f;
    [SerializeField] private GameObject explosionPs;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Vector3 offset;

    private Transform healthBar;
    private Vector3 offsetSprite;

    private void Start()
    {
        healthBar = Instantiate(healthBarPrefab, transform.position + offset, Quaternion.identity).GetComponent<Transform>();
        offsetSprite = healthBar.GetComponentInChildren<SpriteRenderer>().bounds.extents;
    }

    private void LateUpdate()
    {
        HealthBarFollow();
    }

    private void HealthBarFollow()
    {
        healthBar.position = transform.position + offset - offsetSprite;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Instantiate(explosionPs, transform.position, Quaternion.identity);
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
}
