using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    private Image resourceIcon;
    private TMP_Text quantityText;
    private Transform innerBorder;
    private Transform namePanel;

    private InventorySlot slot;

    private void Awake()
    {
        innerBorder = transform.GetChild(0);
        resourceIcon = innerBorder.Find("ResourceIcon").GetComponent<Image>();
        quantityText = innerBorder.Find("Quantity").GetComponent<TMP_Text>();
        namePanel = transform.Find("NamePanel");

        resourceIcon.preserveAspect = true;
        resourceIcon.color = new Color(resourceIcon.color.r, resourceIcon.color.g, resourceIcon.color.b, 0);
    }

    // Update slot, and change image and quantity based on given slot
    public void UpdateUI(InventorySlot slot)
    {
        if (innerBorder == null || resourceIcon == null || quantityText == null || namePanel == null)
        {
            innerBorder = transform.GetChild(0);
            resourceIcon = innerBorder.Find("ResourceIcon").GetComponent<Image>();
            quantityText = innerBorder.Find("Quantity").GetComponent<TMP_Text>();
            namePanel = transform.Find("NamePanel");
        }

        this.slot = slot;
        UpdateIcon();
        UpdateQuantity();
        UpdateName();
    }

    // Update image shown on UI
    private void UpdateIcon()
    {
        if (slot != null)
        {
            resourceIcon.sprite = slot.Item.sprite;
            resourceIcon.color = new Color(resourceIcon.color.r, resourceIcon.color.g, resourceIcon.color.b, 1);
        }
        else
        {
            resourceIcon.sprite = null;
            resourceIcon.color = new Color(resourceIcon.color.r, resourceIcon.color.g, resourceIcon.color.b, 0);
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

    private void UpdateName()
    {
        namePanel.gameObject.SetActive(true);
        TMP_Text nameText = namePanel.Find("NameText").GetComponent<TMP_Text>();
        if (slot != null)
        {
            nameText.text = slot.Item.itemName;
        }
        else
        {
            nameText.text = "";
        }
        namePanel.gameObject.SetActive(false);
    }

    public void UpdateBackround(Color color)
    {
        Image bgImage = innerBorder.GetComponent<Image>();
        bgImage.color = color;
    }
}
