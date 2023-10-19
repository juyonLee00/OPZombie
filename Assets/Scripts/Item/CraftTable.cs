using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftItem
{
    public enum ItemType
    {
        Equipable,
        Consumable,
        Resource
    }
    public enum ItemTypeDetail
    {
        Iron,
        Wood,
        Bone,
        FoodTool,
        Weapon,
        CanGetItem,
        Bed
    }

    public string itemName;
    public string itemDisplayName;
    public Sprite itemImage;

    public string[] needResourcesName;
    public int[] needResourcesNum;
    public GameObject get_ItemPrefab;
}

public class CraftTable : MonoBehaviour
{
    public CraftItem[] craftItemRecipe;
}
