using System.Collections.Generic;

[System.Serializable]
public class DatabaseItem
{
    public int id;
    public string name;
    public string description;
    public string spritePath;
    public int tag;
    public int rarity;
}

[System.Serializable]
public class DatabaseItemList
{
    public List<DatabaseItem> items;
}