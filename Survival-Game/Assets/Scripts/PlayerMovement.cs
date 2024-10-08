using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private PlayerInput playerInput;


    [SerializeField] private float playerSpeed = 7;


    void Awake()
    {
        inputAxis = playerInput.actions.FindAction("Move");

    }

    void FixedUpdate()
    {
        playerMovement();
    }

    private void playerMovement()
    {
        Vector2 movementInput = inputAxis.ReadValue<Vector2>(); //Read input regarding player movement

        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
        direction = transform.TransformDirection(direction); //local to world space

        transform.position += playerSpeed * direction * Time.deltaTime;

    }
}
