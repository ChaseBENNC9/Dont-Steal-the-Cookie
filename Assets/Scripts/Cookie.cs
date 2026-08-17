using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InventoryItem))]

public class Cookie : InteractableObject
{
    public UnityEvent interactEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        // GameObject.Find("Player").GetComponent<PlayerMovement>().PlayAnimation("Pickup");

        if(this.GetComponent<InventoryItem>().Equip(this.GetComponent<InventoryItem>()))
        {
            base.Interact();
            interactable = false;
            this.gameObject.SetActive(false);  
            interactEvent.Invoke();
;
        }




    }
}
