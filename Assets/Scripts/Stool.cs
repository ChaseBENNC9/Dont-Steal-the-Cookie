using UnityEngine;
using UnityEngine.Events;


public class Stool : InteractableObject
{
    private Vector3 lastLocation;
    public GameObject player;
    private CharacterController controller;
    private PlayerMovement movement;
    public Transform pos;

    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        movement = player.GetComponent<PlayerMovement>();
    }
    public void StepOn()
    {
        lastLocation = player.transform.position;
        movement.enableMovement = false;
        controller.enabled = false;
        player.transform.position = pos.position;
        controller.enabled = true;
    }
    public void StepOff()
    {
        
        player.GetComponent<PlayerMovement>().enableMovement = false;
        controller.enabled = false;
        player.transform.position = lastLocation;
        controller.enabled = true;
        movement.enableMovement = true;
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