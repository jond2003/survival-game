using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private PlayerInput playerInput;


    [SerializeField] private float playerSpeed = 7;


    // Start is called before the first frame update
    void Awake()
    {
        inputAxis = playerInput.actions.FindAction("Move");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement();
    }

    private void playerMovement()
    {
        Vector2 direction = inputAxis.ReadValue<Vector2>(); //Read input regarding player movement

        transform.position += playerSpeed * new Vector3(direction.x, 0, direction.y) * Time.deltaTime;

    }
}
