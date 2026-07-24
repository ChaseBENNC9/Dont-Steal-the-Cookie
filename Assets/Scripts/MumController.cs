using UnityEngine;
using System.Collections.Generic;
using System;
using AztechGames;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

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

    private int index;
    
    private void Start()
    {
        index = 0;
        movementController = GetComponent<MoveInBounds>();
        state = MumState.PATROL;
        moveDirection = Vector3.zero;
        movementController.target.position = movementController.waypoint.GetWorldPosition(betterTargetsList[index].targetPosition);

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
                state = MumState.STOPPED;
            
        }
        else
        {
            state = MumState.STOPPED;
        }
        
    }
    private void MumEvent()
    {
        
    }

    public void PatrolRoom()
    {
        ///get room first
        state = MumState.PATROL;
    }

}