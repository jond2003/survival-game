using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private InputAction sprintInput;
    [SerializeField] private InputAction crouchInput;
    [SerializeField] private InputAction jumpInput;

    [SerializeField] private PlayerInput playerInput;




    [SerializeField] private Vector3 velocity;
    [SerializeField] private float gravityForce = -15f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private float playerSpeed = 8;
    [SerializeField] private float playerWalkingSpeed = 8;
    [SerializeField] private float playerSprintSpeed = 11;
    [SerializeField] private float playerCrouchSpeed = 4;


    [SerializeField] private CharacterController characterController;


    [SerializeField] private bool isSprinting = false;
    [SerializeField] private bool isCrouching = false;

    [SerializeField] private AudioSource steppingAudioSource;
 

    void Awake()
    {
        inputAxis = playerInput.actions.FindAction("Move");
        sprintInput = playerInput.actions.FindAction("Sprint");
        crouchInput = playerInput.actions.FindAction("Crouch");
        jumpInput = playerInput.actions.FindAction("Jump");


        steppingAudioSource = GetComponent<AudioSource>();

 
    }

    void FixedUpdate()
    {
        playerMovement();
        checkSprint();
        checkCrouch();
        checkJump();
    }

    private void playerMovement()
    {
        Vector2 movementInput = inputAxis.ReadValue<Vector2>(); //Read input regarding player movement

        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
        direction = transform.TransformDirection(direction); //local to world space

        Vector3 movement = direction * playerSpeed;

  
        if (!characterController.isGrounded) //apply gravity
        {
            velocity.y += gravityForce * Time.deltaTime;

        }

        if (movementInput.magnitude > 0.1f && PauseMenuManager.isPaused == false)
        {
            steppingAudioSource.enabled = true;
        }
        else
        {
            steppingAudioSource.enabled = false;
        }


        movement.y = velocity.y;


        characterController.Move(movement * Time.deltaTime);
        //steppingAudioSource.Play();
    }

    private void checkSprint()
    {
        if ((sprintInput.phase == InputActionPhase.Performed || sprintInput.phase == InputActionPhase.Started) && isCrouching == false && characterController.isGrounded)
        {
            isSprinting = true;
            playerSpeed = playerSprintSpeed;
            steppingAudioSource.pitch = 1.5f;
        }
        else if ((sprintInput.phase == InputActionPhase.Canceled || sprintInput.phase == InputActionPhase.Waiting) && isCrouching == false && isSprinting == true)
        {
            isSprinting = false;
            playerSpeed = playerWalkingSpeed;
            steppingAudioSource.pitch = 1f;
        }
    }

    private void checkCrouch()
    {
        if ((crouchInput.phase == InputActionPhase.Performed || crouchInput.phase == InputActionPhase.Started) && isSprinting == false && characterController.isGrounded)
        {
            isCrouching = true;
            characterController.height = 1.5f;
            playerSpeed = playerCrouchSpeed;
        }
        else if ((crouchInput.phase == InputActionPhase.Canceled || crouchInput.phase == InputActionPhase.Waiting) && isSprinting == false && isCrouching == true )
        {
            isCrouching = false;
            characterController.height = 2f;
            playerSpeed = playerWalkingSpeed;
        }
    }

    private void checkJump()
    {
        if (jumpInput.IsPressed() && characterController.isGrounded)
        {
            velocity.y = jumpForce; 
        }
    }
}
