using UnityEngine;
using System.Collections.Generic;
using System;
using AztechGames;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Events;
using TMPro;
using System.Xml.Serialization;

public enum MumState
{
    STOPPED,
    PATROL
}

public enum WaypointType
{
    CONTINUE,
    END,
    TELEPORT
}



[RequireComponent(typeof(MoveInBounds))]
public class MumController : MonoBehaviour
{
    private MoveInBounds movementController;
    public Vector3 moveDirection;
    [SerializeField] private MumState state;
    [SerializeField] private Animator animator;


    [SerializeField] private List<MumWaypoint> targetsList;
    public GameObject SpeechBubble;
    public TextMeshProUGUI speechText;

    public int index;

    public void SetMumState(int newstate)
    {
        print("SET MUM STATE");
        state = (MumState)newstate;
    }
    private void Start()
    {

        animator.SetBool("Grounded", true);

        movementController = GetComponent<MoveInBounds>();
        SetMumState(1);
        moveDirection = Vector3.zero;
        var sequence = SequenceManager.Instance.TriggerWalkSequence();
        if (sequence != null)
        {
            index = 0;
            targetsList = sequence.walkSequence;
            movementController.target.position = movementController.waypoint.GetWorldPosition(targetsList[index].targetPosition);

        }
    }

    private void Update()
    {
        transform.forward = moveDirection;
        animator.SetFloat("MoveSpeed", moveDirection.magnitude);

        if (state == MumState.PATROL)
        {
            switch (movementController.movementType)
            {
                case MoveInBounds.MovementType.Path: movementController.MoveAlongPath2(); break;
                case MoveInBounds.MovementType.Target: movementController.MoveAlongTarget(); break;
                case MoveInBounds.MovementType.Random: movementController.MoveAlongRandomPath(); break;
            }
        }
    }

    public void SayMessage(string message)
    {
        print("MESSAgGEGE");
        speechText.text = message;
        SpeechBubble.SetActive(true);
    }

    public void PositionReached()
    {
        print("POSITIOON REACHED");
        if (targetsList[index].type == WaypointType.CONTINUE)
        {
            if (index + 1 < targetsList.Count)
                index++;
            movementController.target.position = movementController.waypoint.GetWorldPosition(targetsList[index].targetPosition);
            SetMumState(1);

        }
        else if (targetsList[index].type == WaypointType.TELEPORT)
        {
            if (index + 1 < targetsList.Count)
                index++;
            movementController.target.position = movementController.waypoint.GetWorldPosition(targetsList[index].targetPosition);
            movementController.gameObject.transform.position = movementController.waypoint.GetWorldPosition(targetsList[index].targetPosition);
            // SetMumState(1);

        }
        else if (targetsList[index].type == WaypointType.END)
        {
            SetMumState(0);
            print("STOPPED");
            SequenceManager.Instance.TriggerEndSequence();
            var sequence = SequenceManager.Instance.TriggerWalkSequence();
            if (sequence != null)
            {
                index = 0;
                targetsList = sequence.walkSequence;
                movementController.target.position = movementController.waypoint.GetWorldPosition(targetsList[index].targetPosition);
     

            }



        }



        // else
        // {
        //     SetMumState(0);
        //     print("End");

        // }

    }



}