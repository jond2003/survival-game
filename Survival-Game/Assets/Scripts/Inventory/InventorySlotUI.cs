using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int slotIndex;
    private Image itemImage;
    private Transform originalParent;
    private GameObject draggingItem;

    private Image slotBackground;
    private Color originalColor;
    private Color highlightColor = new Color32(0, 255, 255, 255);

    private void Start()
    {
        slotBackground = GetComponent<Image>();
        originalColor = slotBackground.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Transform itemIcon = transform.Find("ItemIcon");

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
        slotBackground.color = highlightColor;
    }

    // Hover ends
    public void OnPointerExit(PointerEventData eventData)
    {
        slotBackground.color = originalColor;
    }
}
