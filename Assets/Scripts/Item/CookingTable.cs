using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CookingItem
{
    public enum ItemType
    {
        Equipable,
        Consumable,
        Resource
    }
    public enum ItemTypeDetail
    {
        MeatProtein,
        FishProtein,
        Fruit,
        Vegetable,
        Poison,
        Meal,
        Drink,
        FoodCanCook,
    }

    public string itemName;
    public string itemDisplayName;
    public Sprite itemImage;

    public ItemTypeDetail[] needItemType;
    public int[] needItemNumber;

    public GameObject get_ItemPrefab;
}


public class CookingTable : MonoBehaviour
{
    public CookingItem[] cookingItemRecipe;
}
