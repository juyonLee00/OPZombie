using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public ItemData itemData;

    [SerializeField] public float maxCheckDistance;
    public LayerMask layerMask;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collider");
        if(collision.gameObject.tag == "GetItem")
        {
            Debug.Log("collision");
            itemData = collision.gameObject.GetComponent<ItemObject>().GetItemData();
            Debug.Log(itemData.displayName);
            Inventory.instance.AddItem(itemData);
            Destroy(collision.gameObject);
        }
    }

    /*
    void Update()
    {
        RaycastHit2D hit;
        Debug.DrawRay(transform.position, transform.right, Color.magenta);

        if (hit = Physics2D.Raycast(this.transform.position, this.transform.forward, maxCheckDistance))
        {
            if (hit.collider.gameObject.tag == "GetItem" && Input.GetKey(KeyCode.E))
            {
                Debug.Log("collision");
                itemData = hit.collider.gameObject.GetComponent<ItemData>();
                Inventory.instance.AddItem(itemData);
                Destroy(hit.collider.gameObject);
            }
        }
    }*/
}
