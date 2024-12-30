using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
record InventorySlot
{
    public Resource Item { get; set; }
    public int Quantity { get; set; }
}

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InputAction scrollInput;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private GameObject playerHand;

    [SerializeField] private PlayerLook playerLook;

    public bool IsInventoryOpen = false;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private InputAction InventoryInput;

    // Hotbar takes up first maxHotbarItems spaces in inventory array
    [SerializeField] private int maxHotbarItems = 5;
    [SerializeField] private int maxInventoryItems = 25;

    private InventorySlot[] inventory;
    private Dictionary<string, List<int>> itemIndices = new Dictionary<string, List<int>>();

    private Resource heldItem;
    public int hotbarIndex = 0;
    private Image[] hotbarSlots;

    public static PlayerInventory Instance { get; private set; }

    public delegate void ItemChange(Resource r);
    public static event ItemChange OnHeldItemChanged;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        inventory = new InventorySlot[maxInventoryItems];

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

        InventoryInput = playerInput.actions.FindAction("Inventory");

        IncrementHotbarSlot(0);
    }

    void Update()
    {
        if (InventoryInput.WasPerformedThisFrame())
        {
            bool isInventoryActive = !inventoryUI.activeSelf;
            inventoryUI.SetActive(isInventoryActive);

            // Now use the assigned reference
            if (playerLook != null)
            {
                playerLook.SetInventoryOpen(isInventoryActive);
            }

            if (inventoryUI.activeSelf)
            {
                UpdateInventoryUI();
                IsInventoryOpen = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                IsInventoryOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
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

    public void UpdateInventoryUI()
    {
        // Clear existing slots (prevents duplicate items)
        foreach (Transform slot in inventoryUI.transform)
        {
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }

        // Populate slots with items from inventory array
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].Item != null)
            {
                // Instantiate an image for each item in the inventory slot
                GameObject itemIcon = new GameObject("ItemIcon");
                itemIcon.transform.SetParent(inventoryUI.transform.GetChild(i), false);

                Image itemImage = itemIcon.AddComponent<Image>();
                itemImage.sprite = inventory[i].Item.sprite;  // Use the item's sprite
                itemImage.preserveAspect = true;
            }
        }
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
        UnassignHeldItem();

        hotbarSlots[hotbarIndex].color = Color.white;

        hotbarIndex = (maxHotbarItems + hotbarIndex + increment) % maxHotbarItems;
        hotbarSlots[hotbarIndex].color = Color.cyan;

        AssignItemToPlayer();
    }

    // Stores an item in the next free space in the inventory
    // Stacks item if item is already in inventory and is stackable and maxStack not yet reached
    // Returns the index at which it is stored
    // Returns -1 if inventory is full
    public int StoreItem(Resource item)
    {
        bool itemStored = false;
        int indexStoredAt = -1;

        List<int> indices = GetItemIndices(item);

        if (indices.Count > 0)
        {
            int i = 0;
            while (i < indices.Count && !itemStored)
            {
                int index = indices[i];
                InventorySlot slot = inventory[index];
                if (slot.Quantity != item.maxStack)
                {
                    slot.Quantity += 1;
                    indexStoredAt = index;

                    if (index < maxHotbarItems)
                    {
                        UpdateHotbar(i);
                        UpdateInventoryUI();
                    }
                    item.gameObject.SetActive(false);

                    itemStored = true;
                }
                i++;
            }
        }

        if (!itemStored)
        {
            int i = 0;

            while (i < inventory.Length && !itemStored)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = new InventorySlot();
                    inventory[i].Item = item;
                    inventory[i].Quantity = 1;

                    indexStoredAt = i;
                    itemStored = true;

                    Debug.Log("Added " + item.name + " to Inventory!");

                    // If item stored in hotbar slot
                    if (i < maxHotbarItems)
                    {
                        UpdateHotbar(i);
                        UpdateInventoryUI();

                        // Check if item is stored in player's current hotbar slot
                        if (i == hotbarIndex) AssignItemToPlayer();
                        else item.gameObject.SetActive(false);

                    }
                    else  // Hotbar is full
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                i++;
            }
        }

        return indexStoredAt;
    }

    // Gets the indices of the inventory where the item resides
    private List<int> GetItemIndices(Resource item)
    {
        if (itemIndices.TryGetValue(item.itemName, out List<int> indices))
        {
            return indices;
        }
        return new List<int>();
    }

    // Updates hotbar UI
    public void UpdateHotbar(int index)
    {
        if (index < 0 || index >= hotbarSlots.Length)
        {
            Debug.LogWarning($"Hotbar index {index} is out of bounds.");
            return;
        }

        if (inventory[index] != null && inventory[index].Item != null)
        {
            GameObject imageObj = hotbarSlots[index].transform.Find("UIImage")?.gameObject;
            if (imageObj == null)
            {
                imageObj = new GameObject("UIImage");
                imageObj.transform.SetParent(hotbarSlots[index].transform, false);
            }

            Image image = imageObj.GetComponent<Image>() ?? imageObj.AddComponent<Image>();
            image.sprite = inventory[index].Item.sprite;
            image.enabled = true;
        }
        else
        {
            // Clear slot if no item
            Transform imageObj = hotbarSlots[index].transform.Find("UIImage");
            if (imageObj != null)
            {
                Destroy(imageObj.gameObject);
            }
        }
    }

    // Assigns the currently selected hotbar item to the player's hand
    // Initialises the resource to make it usable
    private void AssignItemToPlayer()
    {
        Resource currentItem = inventory[hotbarIndex]?.Item;

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
        }

        heldItem = currentItem;

        if (OnHeldItemChanged != null) OnHeldItemChanged(heldItem);
    }

    // Remove previous assigned item from player hand
    private void UnassignHeldItem()
    {
        if (heldItem != null)
        {
            Collider itemCollider = heldItem.gameObject.GetComponent<Collider>();
            itemCollider.enabled = true;
            heldItem.gameObject.SetActive(false);
            playerHand.transform.DetachChildren();
        }
    }

    public Resource RemoveHeldItem()
    {
        UnassignHeldItem();
        Resource removedItem = RemoveOneItem(hotbarIndex);
        UpdateHotbar(hotbarIndex);
        UpdateInventoryUI();

        return removedItem;
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
        return index > 0 && index < inventory.Length ? inventory[index].Item : null;
    }

    // Uses instance id to get the index of the item in the inventory
    // Returns -1 if no instance is found in the inventory with the same id
    public int GetIndexById(int id)
    {
        int i = 0;
        while (i < inventory.Length)
        {
            Resource item = inventory[i].Item;
            if (item.GetInstanceID() == id)
            {
                return i;
            }
        }
        return -1;
    }

    // Removes one item and returns the item at the given index
    // If there is one item left, it nullifies the index
    public Resource RemoveOneItem(int index)
    {
        InventorySlot slot = inventory[index];
        Resource item = slot.Item;

        if (slot.Quantity == 1) inventory[index] = null;
        else slot.Quantity -= 1;

        return item;
    }

    // Nullifies the index and returns the item at the given object's index
    // Returns null if no object with given id can be found in the inventory
    public Resource RemoveOneItemById(int id)
    {
        int index = GetIndexById(id);
        if (index == -1) return null;

        Resource item = inventory[index].Item;
        inventory[index] = null;
        return item;
    }

    public void ConsumeHeldItem()
    {
        InventorySlot slot = inventory[hotbarIndex];
        Resource item = slot.Item;

        if (slot.Quantity == 1)
        {
            UnassignHeldItem();
            inventory[hotbarIndex] = null;
            Destroy(item.gameObject);
            AssignItemToPlayer();
        }
        else slot.Quantity -= 1;

        UpdateHotbar(hotbarIndex);
    }

    // Gets current selected hotbar item
    public Resource GetHeldItem()
    {
        return inventory[hotbarIndex].Item;
    }

    public void SwapItems(int slotA, int slotB)
    {
        // Swap the items in the inventory array
        InventorySlot temp = inventory[slotA];
        inventory[slotA] = inventory[slotB];
        inventory[slotB] = temp;

        // Update the inventory UI to reflect the changes
        UnassignHeldItem();
        AssignItemToPlayer();
        UpdateInventoryUI(); 
    }
}
