using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.displayName);
    }

    public void OnInteract()
    {
        /*인벤토리에 추가하고 오브젝트 삭제 - 합쳤을 때 주석 제거
        Inventory.instance.AddItem(item);
        Destroy(gameObject);*/
    }
}