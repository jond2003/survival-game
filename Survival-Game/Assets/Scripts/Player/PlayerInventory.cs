using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Hotbar takes up first maxHotbarItems spaces in inventory array
    [SerializeField] private int maxHotbarItems = 5;
    [SerializeField] private int maxInventoryItems = 25;

    private GameObject[] inventory;

    private int hotbarIndex = 0;

    public static PlayerInventory Instance { get; private set; }

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        inventory = new GameObject[maxInventoryItems];

        // Prevent hotbar size larger than inventory size
        if (maxHotbarItems > maxInventoryItems) maxHotbarItems = maxInventoryItems;
    }

    // Returns an array copy of the hotbar portion of the inventory
    public GameObject[] GetHotbar()
    {
        return inventory.Take(maxHotbarItems).ToArray();
    }

    // Returns an array copy of the inventory
    public GameObject[] GetInventory()
    {
        GameObject[] inventoryCopy = new GameObject[maxInventoryItems];
        inventory.CopyTo(inventoryCopy, 0);
        return inventoryCopy;
    }

    // Stores an item in the next free space in the inventory
    // Returns the index at which it is stored
    // Returns -1 if inventory is full
    public int StoreItem(GameObject item)
    {
        int index = 0;
        bool isAppended = false;

        while (index < inventory.Length && !isAppended) {
            if (inventory[index] == null)
            {
                inventory[index] = item;
                isAppended = true;
                Debug.Log("Added " + item.name + " to Inventory!");
            }
            index++;
        }

        return isAppended ? index : -1;
    }

    public GameObject GetItem(int index)
    {
        return index > 0 && index < inventory.Length ? inventory[index] : null;
    }

    // Nullifies the index and returns the item at the given index
    public GameObject RemoveItem(int index)
    {
        GameObject item = inventory[index];
        inventory[index] = null;
        return item;
    }

    // Gets current selected hotbar item
    public GameObject GetHeldItem()
    {
        return inventory[hotbarIndex];
    }
}
