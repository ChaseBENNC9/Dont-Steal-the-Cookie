using UnityEngine;
using System.Collections.Generic;
using System;
using AztechGames;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

public enum MumState
{
    STOPPED,
    NEUTRAL,
    PATROL
}

public enum WaypointType
{
    CONTINUE,
    WAIT
}


[Serializable] public class MumWaypoint
{
    public Vector3 targetPosition;
    public WaypointType type;
}
[RequireComponent(typeof(MoveInBounds))]
public class MumController : MonoBehaviour
{
    private MoveInBounds movementController;
    public Vector3 moveDirection;
    [SerializeField] private MumState state;

    public List<Vector3> targetsList;

    public List<MumWaypoint> betterTargetsList;
    public GameObject SpeechBubble;
    private Text speechText;

    private int index;
    
    private void Start()
    {
        index = 0;
        movementController = GetComponent<MoveInBounds>();
        state = MumState.PATROL;
        moveDirection = Vector3.zero;
        movementController.target.position = movementController.waypoint.GetWorldPosition(betterTargetsList[index].targetPosition);
        speechText = SpeechBubble.GetComponentInChildren<Text>();

    }

    private void Update()
    {
        transform.forward = moveDirection;
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


    public void PositionReached()
    {
        index++;
        if (index < betterTargetsList.Count)
        {
            movementController.target.position = movementController.waypoint.GetWorldPosition(betterTargetsList[index].targetPosition);
            if(betterTargetsList[index-1].type == WaypointType.CONTINUE)
                state = MumState.PATROL;
            else if (betterTargetsList[index-1].type == WaypointType.WAIT)
            {
                state = MumState.STOPPED;
                SpeechBubble.SetActive(true);   
                StartCoroutine(Wait(3));


    
            }
            
        }
        else
        {
            state = MumState.STOPPED;
        }
        
    }
    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SpeechBubble.SetActive(false);   
        state = MumState.PATROL;
    }

    private async Task MumEvent()
    {
    }

    public void PatrolRoom()
    {
        ///get room first
        state = MumState.PATROL;
    }

}