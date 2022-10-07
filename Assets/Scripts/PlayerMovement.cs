using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(0.5f, 10f)] private float moveSpeed = 5;
    [SerializeField][Range(10f, 100f)] private float rotationSpeed = 20;



    private InputActions inputActions;
    private InputAction move;

    private void Awake()
    {
        inputActions = new InputActions();
        move = inputActions.Player.Move;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        MoveKeyboard();

    }
    private void MoveKeyboard()
    {
        var direction = move.ReadValue<Vector2>();
        if(direction.y > 0)
        {
            MoveShipForward(direction.y);
        }
        if(direction.x != 0)
        {
            RotateShip(direction.x);
        }
    }

    private void RotateShip(float rotationMagnitude)
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * rotationMagnitude * -transform.forward);
    }

    private void MoveShipForward(float forwardDirection)
    {
        transform.position += moveSpeed * Time.deltaTime * forwardDirection * transform.up;
    }

    public void DisablePlayerMovement()
    {
        inputActions.Player.Disable();
    }

    public void EnablePlayerMovement()
    {
        inputActions.Player.Enable();
    }
}
