using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject gunInfoPanel;
    public GameObject grenadeInfoPanel;
    public GameObject craftingMenuPanel;
    public GameObject inventoryMenuPanel;
    public GameObject hotbarPanel;
    public GameObject trashPanel;
    public GameObject inactiveCraftingPanel;
    public GameObject gameInfoPanel;

    public TMP_Text heldItemNameText;

    public bool IsInventoryOpen { get; private set; } = false;

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
        hotbarPanel = transform.Find("BackgroundHotbar").gameObject;
        trashPanel = transform.Find("Bin").gameObject;
        inactiveCraftingPanel = craftingMenuPanel.transform.Find("InactiveCraftingPanel").gameObject;
        gameInfoPanel = transform.Find("GameInfo").gameObject;
        heldItemNameText = transform.Find("HeldItemName").GetComponent<TMP_Text>();
    }

    public void ToggleInventory()
    {
        IsInventoryOpen = !IsInventoryOpen;

        inventoryMenuPanel.SetActive(IsInventoryOpen);
        trashPanel.SetActive(IsInventoryOpen);
        craftingMenuPanel.SetActive(IsInventoryOpen);

        Cursor.lockState = IsInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
