using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InputAction scrollInput;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private GameObject playerHand;

    // Hotbar takes up first maxHotbarItems spaces in inventory array
    [SerializeField] private int maxHotbarItems = 5;
    [SerializeField] private int maxInventoryItems = 25;

    private Resource[] inventory;

    private int hotbarIndex = 0;
    private Image[] hotbarSlots;

    public static PlayerInventory Instance { get; private set; }

    public delegate void ItemChange(Resource r);
    public static event ItemChange OnHeldItemChanged;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        inventory = new Resource[maxInventoryItems];

        // Prevent hotbar size larger than inventory size
        if (maxHotbarItems > maxInventoryItems) maxHotbarItems = maxInventoryItems;

        scrollInput = playerInput.actions.FindAction("ScrollWheel");

        hotbarSlots = new Image[maxHotbarItems];

        // Get all hotbar UI images
        int i = 0;
        foreach (Transform child in hotbar.transform)
        {
            hotbarSlots[i] = child.GetComponent<Image>();
            i++;
        }

        IncrementHotbarSlot(0);
    }

    private void OnEnable()
    {
        scrollInput.Enable();
        scrollInput.performed += OnScroll;
    }

    private void OnDisable()
    {
        scrollInput.Disable();
        scrollInput.performed -= OnScroll;
    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<Vector2>().y;

        IncrementHotbarSlot(Math.Sign(-scrollValue));
    }

    // Increment/Decrement the hotbarIndex by the given increment
    // Update hotbar UI and usability of assigned/unassigned hotbar items
    private void IncrementHotbarSlot(int increment)
    {
        hotbarSlots[hotbarIndex].color = Color.white;

        hotbarIndex = (maxHotbarItems + hotbarIndex + increment) % maxHotbarItems;
        hotbarSlots[hotbarIndex].color = Color.cyan;

        // Remove previous assigned item from player hand
        if (playerHand.transform.childCount > 0)
        {
            GameObject oldHeldItem = playerHand.transform.GetChild(0).gameObject;
            Collider itemCollider = oldHeldItem.GetComponent<Collider>();
            itemCollider.enabled = true;
            oldHeldItem.SetActive(false);
            playerHand.transform.DetachChildren();
        }

        AssignItemToPlayer();
    }

    // Stores an item in the next free space in the inventory
    // Returns the index at which it is stored
    // Returns -1 if inventory is full
    public int StoreItem(Resource item)
    {
        int index = 0;
        bool isAppended = false;

        while (index < inventory.Length && !isAppended) {
            if (inventory[index] == null)
            {
                inventory[index] = item;
                isAppended = true;
                Debug.Log("Added " + item.name + " to Inventory!");

                // If item stored in hotbar slot
                if (index < maxHotbarItems)
                {
                    UpdateHotbar(index);

                    // Check if item is stored in player's current hotbar slot
                    if (index == hotbarIndex) AssignItemToPlayer();
                    else item.gameObject.SetActive(false);

                }
                else  // Hotbar is full
                {
                    item.gameObject.SetActive(false);
                }
            }
            index++;
        }

        return isAppended ? index : -1;
    }

    // Updates hotbar UI
    private void UpdateHotbar(int index)
    {
        Resource item = inventory[index];
        if (item != null)
        {
            GameObject imageObj = new GameObject("UIImage");
            imageObj.transform.SetParent(hotbarSlots[index].transform, false);

            Image image = imageObj.AddComponent<Image>();
            image.sprite = item.sprite;
        }
    }

    // Assigns the currently selected hotbar item to the player's hand
    // Initialises the resource to make it usable
    private void AssignItemToPlayer()
    {
        Resource currentItem = inventory[hotbarIndex];

        if (currentItem != null)
        {
            currentItem.gameObject.SetActive(true);
            currentItem.transform.SetParent(playerHand.transform, false);

            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.identity;

            Collider itemCollider = currentItem.GetComponent<Collider>();
            itemCollider.enabled = false;

            IUsable usableItem = currentItem.gameObject.GetComponent<IUsable>();
            usableItem?.Initialise();

            if (OnHeldItemChanged != null) OnHeldItemChanged(currentItem);
        }
    }

    // Returns an array copy of the hotbar portion of the inventory
    public Resource[] GetHotbar()
    {
        return inventory.Take(maxHotbarItems).ToArray();
    }

    // Returns an array copy of the inventory
    public Resource[] GetInventory()
    {
        Resource[] inventoryCopy = new Resource[maxInventoryItems];
        inventory.CopyTo(inventoryCopy, 0);
        return inventoryCopy;
    }

    public Resource GetItem(int index)
    {
        return index > 0 && index < inventory.Length ? inventory[index] : null;
    }

    // Nullifies the index and returns the item at the given index
    public Resource RemoveItem(int index)
    {
        Resource item = inventory[index];
        inventory[index] = null;
        return item;
    }

    // Gets current selected hotbar item
    public Resource GetHeldItem()
    {
        return inventory[hotbarIndex];
    }
}
