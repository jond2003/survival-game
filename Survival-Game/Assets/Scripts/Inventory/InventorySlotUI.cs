using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int slotIndex;
    private Image itemImage;
    private Transform originalParent;
    private GameObject draggingItem;

    private Transform innerBorder;
    private Image slotBackground;
    private Color originalColor;
    private Color highlightColor = Color.gray;

    private void Start()
    {
        Transform innerBorder = transform.Find("InnerBorder");
        if (innerBorder != null)
        {
            slotBackground = innerBorder.GetComponent<Image>();
        }
        else
        {
            slotBackground = GetComponent<Image>();
        }
        originalColor = slotBackground.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (innerBorder == null)
        {
            return;
        }

        Transform itemIcon = innerBorder.Find("ResourceIcon");

        // Check if itemIcon exists
        if (itemIcon == null)
        {
            return;
        }

        itemImage = itemIcon.GetComponent<Image>();

        // Check if itemImage or its sprite is null
        if (itemImage == null || itemImage.sprite == null)
        {
            return;
        }

        draggingItem = new GameObject("DraggingItem");
        draggingItem.transform.SetParent(transform.root);
        draggingItem.transform.SetAsLastSibling();

        Image image = draggingItem.AddComponent<Image>();
        image.sprite = itemImage.sprite;
        image.raycastTarget = false;
        image.preserveAspect = true;

        draggingItem.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        originalParent = transform;
    }

    // Sprite follows the mouse on drag
    public void OnDrag(PointerEventData eventData)
    {
        if (draggingItem != null)
        {
            draggingItem.transform.position = Input.mousePosition;
        }
    }

    // Destroys the Sprite when user lets go of mouse button
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingItem != null)
        {
            Destroy(draggingItem);
        }
    }

    // Swaps the slots
    public void OnDrop(PointerEventData eventData)
    {
        InventorySlotUI draggedSlot = eventData.pointerDrag.GetComponent<InventorySlotUI>();
        if (draggedSlot != null && draggedSlot != this)
        {
            PlayerInventory.Instance.SwapItems(draggedSlot.slotIndex, slotIndex);
            PlayerInventory.Instance.UpdateInventoryUI();

            if (slotIndex < 5)
            {
                PlayerInventory.Instance.UpdateHotbar(slotIndex);
            }
            if (draggedSlot.slotIndex < 5)
            {
                PlayerInventory.Instance.UpdateHotbar(draggedSlot.slotIndex);
            }
        }
    }

    // Hover starts
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowInfo(true);
    }

    // Hover ends
    public void OnPointerExit(PointerEventData eventData)
    {
        ShowInfo(false);
    }

    private void OnDisable()
    {
        ShowInfo(false);
    }

    private void ShowInfo(bool show)
    {
        if (slotIndex < PlayerInventory.Instance.GetInventory().Length)
        {
            slotBackground.color = show ? highlightColor : originalColor;

            if (PlayerInventory.Instance.GetItem(slotIndex) != null)
            {
                Transform namePanel = transform.Find("NamePanel");
                namePanel.gameObject.SetActive(show);
            }
        }
    }
}
