using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int slotIndex;
    private Image itemImage;
    private Transform originalParent;
    private GameObject draggingItem;

    private void Start()
    {
        itemImage = GetComponentInChildren<Image>();
    }

    // Called when the drag starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        // If there's no item in the slot, return early
        if (itemImage.sprite == null) return;

        // Create a new game object to represent the item being dragged
        draggingItem = new GameObject("DraggingItem");
        draggingItem.transform.SetParent(transform.root);
        draggingItem.transform.SetAsLastSibling();

        // Add an image component to the dragging object to show the sprite of the item
        Image image = draggingItem.AddComponent<Image>();
        image.sprite = itemImage.sprite;
        image.raycastTarget = false;
        image.preserveAspect = true;

        originalParent = transform;
    }

    // Called during the drag as the item is being moved
    public void OnDrag(PointerEventData eventData)
    {
        if (draggingItem != null)
        {
            draggingItem.transform.position = Input.mousePosition;
        }
    }

    // Called when the drag ends (either dropped or canceled)
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingItem != null)
        {
            Destroy(draggingItem);

            if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponentInParent<InventorySlotUI>() != null)
            {
                InventorySlotUI targetSlot = eventData.pointerEnter.GetComponentInParent<InventorySlotUI>();
                if (targetSlot != null && targetSlot != this)  // If it's a different slot
                {
                    PlayerInventory.Instance.SwapItems(slotIndex, targetSlot.slotIndex);
                    PlayerInventory.Instance.UpdateInventoryUI();

                    if (slotIndex < 5)
                    {
                        PlayerInventory.Instance.UpdateHotbar(slotIndex);
                    }
                    if (targetSlot.slotIndex < 5)
                    {
                        PlayerInventory.Instance.UpdateHotbar(targetSlot.slotIndex);
                    }
                }
            }
        }
    }

    // Called when an item is dropped onto this slot
    public void OnDrop(PointerEventData eventData)
    {
        InventorySlotUI draggedSlot = eventData.pointerDrag.GetComponent<InventorySlotUI>();
        if (draggedSlot != null && draggedSlot != this)  // Ensure it's a different slot
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
}
