using UnityEngine;

public class Item : ScriptableObject
{
    public int id;
    public Constants.ItemRarity rarity;
    public string name;
    public string description;
    public Constants.ItemTag tag;
    public Sprite sprite;
}
