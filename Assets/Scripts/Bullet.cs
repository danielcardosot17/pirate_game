using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionPs;
    private int damage;
    private Vector3 positionToMoveTo;

    private AudioManager audioManager;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Move(Vector3 direction, float bulletDistance, float bulletTime)
    {

        positionToMoveTo = transform.position + direction * bulletDistance;

        StartCoroutine(LerpPosition(positionToMoveTo, bulletTime));
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        audioManager.PlaySFX("MissOrHit");
        Instantiate(explosionPs, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ShipHealth>() != null)
        {
            var shipHealth = collision.gameObject.GetComponent<ShipHealth>();
            StopAllCoroutines();
            shipHealth.TakeDamage(damage);
            audioManager.PlaySFX("MissOrHit");
            Instantiate(explosionPs, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
