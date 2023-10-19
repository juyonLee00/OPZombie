using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public GameObject inventoryPanel;
    public bool activeInventory = false;

    private void Start()
    {
        inventoryPanel.SetActive(activeInventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    void ShowUI()
    {
        //3개 UI 상자가 보여짐.
        //

    }
}
