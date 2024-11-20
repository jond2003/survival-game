using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private InputAction sprintInput;
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private float playerSpeed = 7;
    [SerializeField] private float playerWalkingSpeed = 7;
    [SerializeField] private float playerSprintSpeed = 10;
    [SerializeField] private Rigidbody rigidBody;


    [SerializeField] private bool isSprinting = false;

    void Awake()
    {
        inputAxis = playerInput.actions.FindAction("Move");
        sprintInput = playerInput.actions.FindAction("Sprint");
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        playerMovement();

  
        checkSprint(sprintInput);


    }

    private void playerMovement()
    {
        Vector2 movementInput = inputAxis.ReadValue<Vector2>(); //Read input regarding player movement

        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
        direction = transform.TransformDirection(direction); //local to world space

        Vector3 rbVelocity = rigidBody.velocity;
        Vector3 newVelocity = playerSpeed * direction.normalized;
        newVelocity.y = rbVelocity.y;  //Allows gravity to work as normal, to prevent floating

        rigidBody.velocity = newVelocity;
    }

    private void checkSprint(InputAction sprintHeld)
    {
        if (sprintHeld.phase == InputActionPhase.Performed)
        {
            isSprinting = true;
            playerSpeed = playerSprintSpeed;
        }
        else
        {
            isSprinting = false;
            playerSpeed = playerWalkingSpeed;
        }
    }
}
