using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public class InteractableObject : MonoBehaviour
{
    public bool interactable;
    public bool interacted;

    protected virtual void Awake()
    {
        interactable = true;
        interacted = false;
    }

    public virtual void Interact()
    {
        interacted = !interacted;
    }
}
