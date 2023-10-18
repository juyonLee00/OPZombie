using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}

public enum ConsumableType
{
    Hp,
    Fatigue,
    Hunger,
    Thirsty,
    Speed,
    Strength,
    Durability,
    Condition//좀비 상태에서 회복 필요
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
    FoodCantCook,
    Iron,
    Wood,
    Bone,
    FoodTool,
    Weapon,
    CanGetItem,
    Bed
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public ItemTypeDetail typeDetail;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

}
