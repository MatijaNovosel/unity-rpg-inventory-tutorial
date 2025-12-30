using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _itemIcon;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public Item ItemInSlot { get; private set; }
    public InventorySlot ActiveSlot { get; set; }
    private Inventory _owner;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemIcon = GetComponent<Image>();
        _itemIcon.raycastTarget = true;
        _rectTransform = GetComponent<RectTransform>();
        _owner = GetComponentInParent<Inventory>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        parent.CurrentInventoryItem = this;
        ActiveSlot = parent;
        ItemInSlot = item;
        _itemIcon.sprite = item.sprite;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_owner.draggablesTransform);
        _canvasGroup.blocksRaycasts = false;
        _itemIcon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Mouse.current.position.ReadValue();
        if (transform.parent != _owner.draggablesTransform)
        {
            transform.SetParent(_owner.draggablesTransform);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _itemIcon.raycastTarget = true;
        transform.SetParent(ActiveSlot.transform, false);
        ((RectTransform)transform).anchoredPosition = Vector2.zero;
    }
}