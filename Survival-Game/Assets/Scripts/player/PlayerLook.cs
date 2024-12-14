using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private PlayerInput playerInput;
    private Transform playerTransform;

    [SerializeField] private float cameraSensitivity = 4;
    float xRotate = 0f;
    float yRotate = 0f;

    void Awake()
    {

        Cursor.lockState = CursorLockMode.Locked; //Hide cursor

        inputAxis = playerInput.actions.FindAction("Look");
        playerTransform = transform.parent;
        
    }

    void Update()
    {
        playerLook();
    }

    void FixedUpdate()
    {
        playerRotate();
    }

    private void playerLook()
    {
        Vector2 direction = inputAxis.ReadValue<Vector2>(); //Read mouse movement

        float mouseX = cameraSensitivity * direction.x * Time.deltaTime;
        float mouseY = cameraSensitivity * direction.y * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        yRotate += mouseX;

        transform.localRotation = Quaternion.Euler(xRotate, 0, 0);
    }

    private void playerRotate()
    {
        playerTransform.Rotate(Vector3.up * yRotate); //rotate on horizontal
        yRotate = 0f;
    }
}
