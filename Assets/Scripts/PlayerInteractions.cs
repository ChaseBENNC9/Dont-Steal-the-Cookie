using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour
{

    public InteractableObject target;
    public List<InteractableObject> nearbyInteractables = new List<InteractableObject>();

    void Update()
    {
        
        if( nearbyInteractables.Count != 0)
        {
            
            target = nearbyInteractables[nearbyInteractables.Count - 1];
        }
        else
        {
            target = null;
        }
        if (target != null && !target.interactable)
        {
            nearbyInteractables.Remove(target);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<InteractableObject>(out var obj) && obj.interactable && !nearbyInteractables.Contains(obj))
        {
            nearbyInteractables.Add(obj);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<InteractableObject>(out var obj))
        {
            if(nearbyInteractables.Contains(obj))
            {
                nearbyInteractables.Remove(obj);
            }
        }
    }


    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started && target)
        {
            target.Interact();
        }
    }
}
