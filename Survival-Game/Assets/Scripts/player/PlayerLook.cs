using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private PlayerInput playerInput;
    private Transform playerTransform;

    [SerializeField] private float cameraSensitivity = 4;
    float xRotate = 0f;

    void Awake()
    {

        Cursor.lockState = CursorLockMode.Locked; //Hide cursor

        inputAxis = playerInput.actions.FindAction("Look");
        playerTransform = transform.parent;         
        
    }

    void FixedUpdate()
    {
        playerLook();
    }

    private void playerLook()
    {
        Vector2 direction = inputAxis.ReadValue<Vector2>(); //Read mouse movement

        float mouseX = cameraSensitivity * direction.x * Time.deltaTime;
        float mouseY = cameraSensitivity * direction.y * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotate, 0, 0);
        playerTransform.Rotate(Vector3.up * mouseX); //rotate on horizontal


    }
}
