using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem CurrentInventoryItem { get; set; }
    public Constants.ItemTag Tag;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var droppedItem = eventData.pointerDrag?.GetComponent<InventoryItem>();
        if (droppedItem is null) return;

        if (Tag != Constants.ItemTag.None && droppedItem.ItemInSlot.tag != Tag)
        {
            return;
        }

        var fromSlot = droppedItem.ActiveSlot;
        var toSlot = this;

        if (toSlot.CurrentInventoryItem is null)
        {
            MoveItem(droppedItem, toSlot);
            return;
        }

        SwapItems(fromSlot, toSlot);
    }

    private static void MoveItem(InventoryItem item, InventorySlot targetSlot)
    {
        if (item.ActiveSlot) item.ActiveSlot.CurrentInventoryItem = null;

        item.ActiveSlot = targetSlot;
        targetSlot.CurrentInventoryItem = item;

        item.transform.SetParent(targetSlot.transform, false);
        ((RectTransform)item.transform).anchoredPosition = Vector2.zero;
    }
    
    private static void SwapItems(InventorySlot slotA, InventorySlot slotB)
    {
        (slotA.CurrentInventoryItem, slotB.CurrentInventoryItem) = (slotB.CurrentInventoryItem, slotA.CurrentInventoryItem);

        if (slotA.CurrentInventoryItem != null)
        {
            slotA.CurrentInventoryItem.ActiveSlot = slotA;
            slotA.CurrentInventoryItem.transform.SetParent(slotA.transform, false);
            ((RectTransform)slotA.CurrentInventoryItem.transform).anchoredPosition = Vector2.zero;
        }

        if (slotB.CurrentInventoryItem == null) return;
        
        slotB.CurrentInventoryItem.ActiveSlot = slotB;
        slotB.CurrentInventoryItem.transform.SetParent(slotB.transform, false);
        ((RectTransform)slotB.CurrentInventoryItem.transform).anchoredPosition = Vector2.zero;
    }
}