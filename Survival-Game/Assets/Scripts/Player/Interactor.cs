using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Interfaces all generic interactable objects
interface IInteractable
{
    public void Interact();  // Interact with object
    public void Highlight(bool isOn);  // Indicate that object is interactable
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private InputAction interactInput;
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private Transform interactSource;
    [SerializeField] private float interactRange;

    void Awake()
    {
        interactInput = playerInput.actions.FindAction("Interact");
    }

    void Update()
    {
        // Shoot ray to get object player is looking at
        RaycastHit hit;
        if (Physics.Raycast(interactSource.position, interactSource.forward, out hit, interactRange))
        {
            // Check if object is interactable
            IInteractable interactable = null;
            if (hit.transform.gameObject.TryGetComponent(out IInteractable newInteractable))
            {
                // Set new interactable and indicate to user that they are looking at an interactable
                if (newInteractable != interactable)
                {
                    interactable = newInteractable;
                    interactable.Highlight(true);
                }

                // Interact with interactable when interact button pressed
                if (interactInput.IsPressed())
                {
                    interactable.Interact();
                }
            } else
            {
                // Remove highlight after looking away from interactable
                interactable?.Highlight(false);
            }
        }
    }
}
