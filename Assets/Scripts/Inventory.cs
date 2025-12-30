using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] public Transform draggablesTransform;
    [SerializeField] private InventoryItem itemPrefab;

    private void Start()
    {
        if (name == "Hotbar")
        {
            //
        }
        else if (name == "Inventory")
        {
            SpawnItem(Database.Singleton.GetItem(1));
        }
    }

    private void SpawnItem(Item item = null, int? index = null)
    {
        if (index != null)
        {
            var slot = inventorySlots[(int)index];
            if (slot.CurrentInventoryItem) return;
            var newItem = Instantiate(itemPrefab, slot.transform);
            newItem.Initialize(item, slot);
            return;
        }

        foreach (var slot in inventorySlots)
        {
            if (slot.CurrentInventoryItem) continue;
            var newItem = Instantiate(itemPrefab, slot.transform);
            newItem.Initialize(item, slot);
            break;
        }
    }
}