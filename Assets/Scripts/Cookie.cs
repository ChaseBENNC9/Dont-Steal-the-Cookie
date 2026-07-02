using UnityEngine;

public class Cookie : InteractableObject
{
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
        base.Interact();
        print(myThing);
        interactable = false;
        this.gameObject.SetActive(false);
    
    }
}
