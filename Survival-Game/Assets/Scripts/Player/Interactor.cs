using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

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

    private IInteractable interactable;

    private bool shouldRegisterInput = false;

    void Awake()
    {
        interactInput = playerInput.actions.FindAction("Interact");
    }

    void Update()
    {
        // Shoot ray to get object player is looking at and check if object is interactable
        if (
            Physics.Raycast(interactSource.position, interactSource.forward, out RaycastHit hit, interactRange) &&
            hit.transform.gameObject.TryGetComponent(out IInteractable newInteractable))
        {
            // Set new interactable and indicate to user that they are looking at an interactable
            if (newInteractable != interactable)
            {
                interactable?.Highlight(false);  // Unhighlight old interactable

                interactable = newInteractable;
                interactable.Highlight(true);
            }

            // Interact with interactable when interact button pressed
            if (interactInput.IsPressed() && shouldRegisterInput)
            {
                interactable.Interact();
            }

            // Reset input
            shouldRegisterInput = !interactInput.IsPressed();  // need to release input button to be registered
        }
        else
        {
            NoInteraction();
        }
    }

    // Removes highlights, nullifies the current interactable and resets interaction inputs
    void NoInteraction()
    {
        // Remove highlight after looking away from interactable
        interactable?.Highlight(false);
        interactable = null;
        shouldRegisterInput = false;
    }
}
