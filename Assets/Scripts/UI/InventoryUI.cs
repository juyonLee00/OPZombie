using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventoryInfo;
    public bool activeInventory = false;

    private void Start()
    {
        inventoryPanel.SetActive(activeInventory);
        inventoryInfo.SetActive(activeInventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activeInventory = !activeInventory;
            inventoryInfo.SetActive(activeInventory);
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
