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

// Interfaces all generic usable objects
interface IUsable
{
    public void Initialise();  // Initialise to make usable
    public void LMB_Action(bool isPressed);  // Left Mouse Button Action
    public void RMB_Action(bool isPressed);  // Right Mouse Button Action
    public void ReloadAction(bool isPressed);  // Reload (R) Action
    public void Uninitialise();  // Unitialise to make unusable
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactRange;

    [SerializeField] private PlayerInput playerInput;
    private InputAction interactInput;
    private InputAction lmbInput;
    private InputAction rmbInput;
    private InputAction reloadInput;


    private IInteractable interactable;
    private IUsable heldItem;

    void Awake()
    {
        interactInput = playerInput.actions.FindAction("Interact");
        lmbInput = playerInput.actions.FindAction("LMB");
        rmbInput = playerInput.actions.FindAction("RMB");
        reloadInput = playerInput.actions.FindAction("Reload");
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
        // Skip interaction checks if the inventory is open
        if (PlayerInventory.Instance.IsInventoryOpen)
        {
            return;  
        }


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
        if (interactInput.WasPressedThisFrame())
        {
            interactable.Interact();
        }
    }

    // Removes highlights and nullifies the current interactable
    void NoInteraction()
    {
        // Remove highlight after looking away from interactable
        interactable?.Highlight(false);
        interactable = null;
    }

    void SetCurrentItem(Resource item)
    {
        heldItem = item != null ? item.gameObject.GetComponent<IUsable>() : null;
    }

    void CheckInputs()
    {
        if (lmbInput.WasPressedThisFrame())
        {
            heldItem.LMB_Action(true);
        }
        else if (lmbInput.WasReleasedThisFrame())
        {
            heldItem.LMB_Action(false);
        }

        if (rmbInput.WasPressedThisFrame())
        {
            heldItem.RMB_Action(true);
        }
        else if (rmbInput.WasReleasedThisFrame())
        {
            heldItem.RMB_Action(false);
        }

        if (reloadInput.WasPressedThisFrame())
        {
            heldItem.ReloadAction(true);
        }
        else if (reloadInput.WasReleasedThisFrame())
        {
            heldItem.ReloadAction(false);
        }
    }
}
