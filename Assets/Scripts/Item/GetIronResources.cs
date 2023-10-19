using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIronResources : MonoBehaviour
{
    public GameObject itemToGive;
    public int quantityPerHit = 3;
    public int capacity = 0;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Hammer")
        {
            if(capacity >= quantityPerHit)
            {
                Destroy(gameObject);
            }

            capacity += 1;
            Instantiate(itemToGive, transform.position, Quaternion.identity);
            
            }

        }
}
