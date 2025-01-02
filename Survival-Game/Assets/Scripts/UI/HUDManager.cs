using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject gunInfoPanel;
    public GameObject grenadeInfoPanel;
    public GameObject craftingMenuPanel;
    public GameObject inventoryMenuPanel;

    public bool IsInventoryOpen { get; private set; } = false;
    public bool IsCraftingMenuOpen { get; private set; } = false;

    public static HUDManager Instance { get; private set; }
    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        gunInfoPanel = transform.Find("GunInfo").gameObject;
        grenadeInfoPanel = transform.Find("GrenadeInfo").gameObject;
        craftingMenuPanel = transform.Find("BackgroundCraftingMenu").gameObject;
        inventoryMenuPanel = transform.Find("BackgroundInventory").gameObject;
    }

    public void ToggleInventory()
    {
        IsInventoryOpen = !IsInventoryOpen;
        inventoryMenuPanel.SetActive(IsInventoryOpen);
        Cursor.lockState = IsInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        if (IsCraftingMenuOpen && !IsInventoryOpen) ToggleCraftingMenu();
    }

    public void ToggleCraftingMenu()
    {
        IsCraftingMenuOpen = !IsCraftingMenuOpen;
        craftingMenuPanel.SetActive(IsCraftingMenuOpen);
        if ((IsInventoryOpen && !IsCraftingMenuOpen) || (!IsInventoryOpen && IsCraftingMenuOpen)) ToggleInventory();
    }
}
