using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 0.0f;
    private float distanceToMove = 0.0f;
    private float timeToMove = 0.0f;

    public void Move(float bulletDistance, float bulletTime)
    {
        distanceToMove = bulletDistance;
        timeToMove = bulletTime;

    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
