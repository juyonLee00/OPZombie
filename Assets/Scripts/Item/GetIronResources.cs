using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIronResources : MonoBehaviour
{
    public ItemData itemToGive;
    public int quantityPerHit = 3;
    public int capacity = 0;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Hammer")
        {
            Debug.Log("capacity");
            capacity += 1;

            if(capacity == quantityPerHit)
            {
                for(int i=0; i<quantityPerHit; i++)
                {
                    Instantiate(itemToGive.dropPrefab, transform.position, Quaternion.identity);
                }
            }

            Destroy(gameObject);
        }
    }
}
