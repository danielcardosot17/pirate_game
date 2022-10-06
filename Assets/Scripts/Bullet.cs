using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;

    public Vector3 positionToMoveTo;

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
    }
}
