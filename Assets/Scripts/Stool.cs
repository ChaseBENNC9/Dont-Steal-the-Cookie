using UnityEngine;
using UnityEngine.Events;


public class Stool : InteractableObject
{
    private Vector3 lastLocation;
    public GameObject player;
    private CharacterController controller;
    private PlayerMovement movement;
    public Transform pos;
    private Rigidbody rb;

    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        movement = player.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }
    public void StepOn()
    {
        lastLocation = player.transform.position;
        movement.enableMovement = false;
        controller.enabled = false;
        player.transform.position = pos.position;
        controller.enabled = true;
        rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
    public void StepOff()
    {
        
        controller.enabled = false;
        player.transform.position = lastLocation;
        controller.enabled = true;
        movement.enableMovement = true;
        rb.constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ);

    }
    public override void Interact()
    {
        base.Interact();
        if(interacted)
        {
            StepOn();
        }
        else
        {
            StepOff();
        }
    
    }
}