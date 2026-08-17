using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.Events;


[Serializable] public class MumWaypoint
{
    public Vector3 targetPosition;
    public WaypointType type;
    
}
[CreateAssetMenu(fileName = "Mum", menuName = "Walk Sequence")]
public class WalkSequence : ScriptableObject
{
    public List<MumWaypoint> walkSequence;
    public bool autoTrigger;




}
