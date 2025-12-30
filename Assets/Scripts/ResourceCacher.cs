using UnityEngine;

public class ResourceCacher : MonoBehaviour
{
    public static ResourceCacher Singleton;
    public Sprite[] ArmorAndWeaponSprites;
    
    private void Awake()
    {
        Singleton = this;
        ArmorAndWeaponSprites = Resources.LoadAll<Sprite>("Sprites/armorAndWeapons");
    }
}
