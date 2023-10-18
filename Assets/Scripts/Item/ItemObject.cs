using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        Debug.Log("Pickup {0}");
        return string.Format("Pickup {0}", item.displayName);
    }

    public void OnInteract()
    {
        //Inventory.instance.AddItem(item);
        //Destroy(gameObject);
    }
}