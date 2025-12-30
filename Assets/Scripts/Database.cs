using System.Linq;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database Singleton;
    private DatabaseItemList _databaseItemList;

    private void Awake()
    {
        Singleton = this;
        _loadItems();
    }

    public Item GetItem(int id)
    {
        var databaseItem = _databaseItemList.items.FirstOrDefault(x => x.id == id);
        if (databaseItem is null) return null;
        
        var item = ScriptableObject.CreateInstance<Item>();
        
        item.id = databaseItem.id;
        item.name = databaseItem.name;
        item.description = databaseItem.description;
        item.tag = (Constants.ItemTag)databaseItem.tag;
        item.sprite = ResourceCacher.Singleton.ArmorAndWeaponSprites.First(x => x.name == databaseItem.spritePath);
        item.rarity = (Constants.ItemRarity)databaseItem.rarity;
        
        return item;
    }

    private void _loadItems()
    {
        var jsonFile = Resources.Load<TextAsset>("Misc/Items");
    
        if (!jsonFile)
        {
            Debug.LogError("Item JSON not found!");
            return;
        }
    
        _databaseItemList = JsonUtility.FromJson<DatabaseItemList>(jsonFile.text);
    }
}