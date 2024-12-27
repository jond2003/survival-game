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
    [SerializeField] private float interactRange;

    [SerializeField] private PlayerInput playerInput;
    private InputAction interactInput;
    private InputAction lmbInput;
    private InputAction rmbInput;


    private IInteractable interactable;
    private IUsable heldItem;

    private bool registerInteract = false;

    void Awake()
    {
        interactInput = playerInput.actions.FindAction("Interact");
        lmbInput = playerInput.actions.FindAction("LMB");
        rmbInput = playerInput.actions.FindAction("RMB");
    }

    void OnEnable()
    {
        PlayerInventory.OnHeldItemChanged += SetCurrentItem;
    }

    void OnDisable()
    {
        PlayerInventory.OnHeldItemChanged -= SetCurrentItem;
    }

    void Update()
    {
        if (heldItem != null)
        {
            CheckInputs();
        }

        // Shoot ray to get object player is looking at and check if object is interactable
        if (
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange) &&
            hit.transform.gameObject.TryGetComponent(out IInteractable newInteractable))
        {
            Interaction(newInteractable);
        }
        else
        {
            NoInteraction();
        }
    }

    void Interaction(IInteractable newInteractable)
    {
        // Set new interactable and indicate to user that they are looking at an interactable
        if (newInteractable != interactable)
        {
            interactable?.Highlight(false);  // Unhighlight old interactable

            interactable = newInteractable;
            interactable.Highlight(true);
        }

        // Interact with interactable when interact button pressed
        if (interactInput.IsPressed() && registerInteract)
        {
            interactable.Interact();
        }

        // Reset input
        registerInteract = !interactInput.IsPressed();  // need to release input button to be registered
    }

    // Removes highlights, nullifies the current interactable and resets interaction inputs
    void NoInteraction()
    {
        // Remove highlight after looking away from interactable
        interactable?.Highlight(false);
        interactable = null;
        registerInteract = false;
    }

    void SetCurrentItem(Resource item)
    {
        heldItem = item.gameObject.GetComponent<IUsable>();
    }

    void CheckInputs()
    {
        if (lmbInput.IsPressed())
        {
            heldItem.LMB_Action();
        }

        if (rmbInput.IsPressed())
        {
            heldItem.RMB_Action();
        }
    }
}
