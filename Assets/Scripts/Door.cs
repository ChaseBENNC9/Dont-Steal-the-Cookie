using UnityEngine;


public class Door : InteractableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Key myKey;
    public bool isLocked;
    [SerializeField] private PlayerInventory playerInventory;
    protected void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        Debug.Assert(playerInventory != null, "Player does not have an inventory component attached");
        Debug.Assert(!isLocked || myKey != null, "Door requires a key to be locked!! " + this.gameObject.name);

    }


    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        base.Interact();

        if (isLocked)
        {


            if (playerInventory.currentItem != null)
            {
                if (playerInventory.currentItem.GetComponent<InteractableObject>() == myKey)
                {


                    isLocked = false;

                }
                else
                {

                }
            }

            else
            {
            }
        }
        else
        {
        }
    }
}
