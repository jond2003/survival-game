using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private PlayerInput playerInput;
    private Transform playerTransform;

    [SerializeField] private float cameraSensitivity;
    float newSensitivity = 0f;
    float xRotate = 0f;
    float yRotate = 0f;

    private bool isInventoryOpen = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; //Hide cursor

        inputAxis = playerInput.actions.FindAction("Look");
        playerTransform = transform.parent;
    }

    void Update()
    {
        // Only allow looking around if inventory is not open
        if (!PauseMenuManager.isPaused && !isInventoryOpen)
        {
            playerLook();
        }
    }

    void FixedUpdate()
    {
        // Only allow rotating when inventory is not open
        if (!isInventoryOpen)
        {
            playerRotate();
        }
    }

    private void playerLook()
    {

        newSensitivity = cameraSensitivity * PlayerPrefs.GetFloat("Sensitivity", 1);
        Vector2 direction = inputAxis.ReadValue<Vector2>(); //Read mouse movement

        float mouseX = newSensitivity * direction.x;
        float mouseY = newSensitivity * direction.y;

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

    // This method will be called when the inventory opens/closes
    public void SetInventoryOpen(bool isOpen)
    {
        isInventoryOpen = isOpen;

        // Optionally, unlock the cursor when the inventory is open
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
