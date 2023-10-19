using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDurability : MonoBehaviour
{
    [SerializeField]
    GameObject item;
    ItemObject itemObject;
    public int durability = 100;


    private void Awake()
    {
        itemObject = item.GetComponent<ItemObject>();

    }  
    

    //좀비 또는 originIron과 충돌하면 내구도 10씩 감소
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //좀비와 충돌했을 때도 추가 예정
        if(collision.gameObject.name == "OriginIron")
        {
            durability -= 10;

            if(durability <= 0)
            {
                Destroy(item);
                //인벤토리서도 없어지나?
            }
        }
    }
}
