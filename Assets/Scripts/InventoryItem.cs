
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class InventoryItem : MonoBehaviour
{
    public string itemName;
    private PlayerInventory playerInventory;

    private void Start()
    {
        itemName = itemName.ToLower();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        Debug.Assert(playerInventory != null,"Player does not have an inventory component attached");
    }
    public bool Equip(InventoryItem item)
    {
        if (playerInventory.currentItem == null)
        {
            playerInventory.currentItem = item;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool UnEquip(InventoryItem item)
    {
        if (playerInventory.currentItem == item)
        {
            playerInventory.currentItem = null;
            return true;
        }
        else
        {
            return false;
        }
    }
}
