using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private float playerSpeed = 7;
    [SerializeField] private Rigidbody rigidBody;


    void Awake()
    {
        inputAxis = playerInput.actions.FindAction("Move");
        rigidBody = gameObject.GetComponent<Rigidbody>();
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

        Vector3 rbVelocity = rigidBody.velocity;
        Vector3 newVelocity = playerSpeed * direction.normalized;
        newVelocity.y = rbVelocity.y;  //Allows gravity to work as normal, to prevent floating

        rigidBody.velocity = newVelocity;
    }
}
