using UnityEngine;

public class HidingSpot : InteractableObject
{
    public GameObject player;
    private const int DEFAULT_LAYER = 0;
    private const int HIDDEN_LAYER = 3;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        player.GetComponent<PlayerMovement>().enableMovement = false;
        HideObject(player,HIDDEN_LAYER);
    }
    public void StopHiding()
    {
        
        player.GetComponent<PlayerMovement>().enableMovement = true;
        HideObject(player,DEFAULT_LAYER);


    }
    public override void Interact()
    {
        base.Interact();
        if(interacted)
        {
            Hide();
        }
        else
        {
            StopHiding();
        }
    
    }

    private void HideObject(GameObject gameObject,int layer)
    {
         gameObject.layer = layer; 
           foreach (Transform child in gameObject.transform)
         {
                 child.gameObject.layer = layer;
         }
    }
}
