using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField][Range(1, 10)] private int frontalDamage = 5;
    [SerializeField][Range(1, 10)] private int sideDamage = 2;

    [SerializeField][Range(1f, 10f)] private float bulletDistance = 5f;
    [SerializeField][Range(0.1f, 2f)] private float bulletTime = 0.5f;
    [SerializeField][Range(0.5f, 3f)] private float attackReloadTime = 1f;

    [SerializeField] private Transform frontalSpawnPoint;
    [SerializeField] private Transform leftSpawnPoints;
    [SerializeField] private Transform rightSpawnPoints;

    [SerializeField] private GameObject frontalBullet;
    [SerializeField] private GameObject sideBullet;

    private AudioManager audioManager;

    private float timer = 0.0f;
    private bool isReady = true;


    private InputActions inputActions;
    private InputAction attack;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        inputActions = new InputActions();
        attack = inputActions.Player.Attack;
        timer = attackReloadTime;
    }

    private void OnEnable()
    {
        attack.performed += OnAttack;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }


    private void OnAttack(InputAction.CallbackContext obj)
    {
        if(isReady)
        {
            audioManager.PlaySFX("Attack");
            FrontalAttack();
            SideAttack(leftSpawnPoints, -transform.right);
            SideAttack(rightSpawnPoints, transform.right);
            isReady = false;
            timer = attackReloadTime;
        }
    }

    private void Update()
    {
        if(!isReady)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0.0f;
                isReady = true;
            }
        }
    }

    private void FrontalAttack()
    {
        var frontalCannonBall = Instantiate(frontalBullet, frontalSpawnPoint.position, Quaternion.identity);
        var bullet = frontalCannonBall.GetComponent<Bullet>();
        bullet.SetDamage(frontalDamage);
        bullet.Move(transform.up ,bulletDistance, bulletTime);
    }

    private void SideAttack(Transform spawnPoints, Vector3 direction)
    {
        foreach(Transform spawnpoint in spawnPoints)
        {
            var sideCannonBall = Instantiate(sideBullet, spawnpoint.position, Quaternion.identity);
            var bullet = sideCannonBall.GetComponent<Bullet>();
            bullet.SetDamage(sideDamage);
            bullet.Move(direction, bulletDistance, bulletTime);
        }
    }

    public void DisablePlayerAttack()
    {
        inputActions.Player.Disable();
    }

    public void EnablePlayerAttack()
    {
        inputActions.Player.Enable();
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bulletDistance);
    }
}
