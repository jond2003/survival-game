using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public record InventorySlot
{
    public Resource Item;
    public int Quantity;
}

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InputAction scrollInput;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private GameObject playerHand;

    private GameObject inventoryUI;
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

    private void Start()
    {
        inventoryUI = HUDManager.Instance.inventoryMenuPanel;
    }

    void Update()
    {
        if (InventoryInput.WasPerformedThisFrame())
        {
            HUDManager.Instance.ToggleInventory();

            if (HUDManager.Instance.IsInventoryOpen)
            {
                UpdateInventoryUI();
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
        foreach(Transform slot in inventoryUI.transform)
{
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);  // Destroy all children
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

                    if (itemIndices.TryGetValue(item.itemName, out List<int> inventoryItemPositions)) inventoryItemPositions.Add(i);
                    else itemIndices.Add(item.itemName, new List<int>(new int[] { i }));

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
            image.preserveAspect = true;
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
            IUsable usableItem = heldItem.gameObject.GetComponent<IUsable>();
            usableItem?.Uninitialise();
            heldItem.gameObject.SetActive(false);
            playerHand.transform.DetachChildren();
        }
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

    public void ConsumeHeldItem()
    {
        InventorySlot slot = inventory[hotbarIndex];
        Resource item = slot.Item;

        if (slot.Quantity == 1)
        {
            UnassignHeldItem();
            inventory[hotbarIndex] = null;
            itemIndices.Remove(item.itemName);
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

    public void SwapItems(int slotAIndex, int slotBIndex)
    {
        InventorySlot slotA = inventory[slotAIndex];
        InventorySlot slotB = inventory[slotBIndex];

        // Return early if both slots are empty (nothing to swap)
        if (slotA == null && slotB == null) return;

        // Update item indices safely
        if (slotA != null && itemIndices.TryGetValue(slotA.Item.itemName, out List<int> slotAIndices))
        {
            int oldSlotIndex = slotAIndices.FindIndex(i => i == slotAIndex);
            slotAIndices[oldSlotIndex] = slotBIndex;
        }

        if (slotB != null && itemIndices.TryGetValue(slotB.Item.itemName, out List<int> slotBIndices))
        {
            int oldSlotIndex = slotBIndices.FindIndex(i => i == slotBIndex);
            slotBIndices[oldSlotIndex] = slotAIndex;
        }

        // Perform the swap
        inventory[slotAIndex] = slotB;
        inventory[slotBIndex] = slotA;

        // Update the inventory UI to reflect the changes
        UnassignHeldItem();
        AssignItemToPlayer();
        UpdateInventoryUI();
    }

    public int GetStackQuantity(int index)
    {
        return inventory[index].Quantity;
    }

    public int HasItem(string itemName)
    {
        int quantity = 0;
        if (itemIndices.TryGetValue(itemName, out List<int> indices))
        {
            foreach (int i in indices)
            {
                quantity += inventory[i].Quantity;
            }
        }
        return quantity;
    }
}
