using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{

    private InteractableObject target;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<InteractableObject>(out var obj))
        {
            target = obj; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<InteractableObject>(out var obj))
        {
            if( obj == target)
            {
                target = null;
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
