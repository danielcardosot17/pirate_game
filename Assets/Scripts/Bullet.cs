using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private float distanceToMove;
    private float timeToMove;
    private Vector3 directionToMove;

    public void Move(Vector3 direction, float bulletDistance, float bulletTime)
    {
        distanceToMove = bulletDistance;
        timeToMove = bulletTime;
        directionToMove = direction;

    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
