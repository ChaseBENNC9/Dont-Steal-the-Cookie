using UnityEngine;


public class Door : InteractableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Key myKey;
    public bool isLocked;
    public Transform doorTransform;
    [SerializeField] private PlayerInventory playerInventory;
    public GameObject padlock;
    protected void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        Debug.Assert(playerInventory != null, "Player does not have an inventory component attached");
        Debug.Assert(!isLocked || myKey != null, "Door requires a key to be locked!! " + this.gameObject.name);
        if (doorTransform == null)
        {
            doorTransform = this.transform;
        }
        padlock.SetActive(isLocked);

    }


    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {

        if (isLocked)
        {
            if (playerInventory.currentItem != null)
            {
                if (playerInventory.currentItem.GetComponent<Key>() == myKey)
                {
                    isLocked = false;
                    playerInventory.currentItem.GetComponent<Key>().Use();
                    padlock.SetActive(isLocked);

                }
            }
        }
        else
        {
            base.Interact();
            ToggleDoor();
        }
    }


    public void ToggleDoor()
    {
        if(interacted)
        {
            doorTransform.Rotate(new Vector3(0,1,0),90);
        }
        else
        {
            doorTransform.Rotate(new Vector3(0,1,0),-90);
        }
    }
}
