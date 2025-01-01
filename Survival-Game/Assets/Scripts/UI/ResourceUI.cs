using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    private Image resourceIcon;
    private TMP_Text quantityText;

    private InventorySlot slot;

    private void Awake()
    {
        resourceIcon = transform.GetChild(0).Find("ResourceIcon").GetComponent<Image>();
        quantityText = transform.GetChild(0).Find("Quantity").GetComponent<TMP_Text>();

        resourceIcon.preserveAspect = true;
    }

    // Update slot, and change image and quantity based on given slot
    public void UpdateUI(InventorySlot slot)
    {
        this.slot = slot;
        UpdateIcon();
        UpdateQuantity();
    }

    // Update image shown on UI
    private void UpdateIcon()
    {
        if (slot != null)
        {
            resourceIcon.sprite = slot.Item.sprite;
        }
        else
        {
            resourceIcon.sprite = null;
        }
    }


    // Update item quantity shown on UI
    public void UpdateQuantity()
    {
        if (slot != null)
        {
            quantityText.text = slot.Quantity.ToString();
        }
        else
        {
            quantityText.text = "";
        }
    }
}
