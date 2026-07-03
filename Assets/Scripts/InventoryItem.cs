
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
            Debug.Log("Acquired " + item.name);
            return true;
        }
        else
        {
            Debug.Log("Something already equipped");
            return false;
        }
    }
}
