using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
namespace AztechGames
{
    public class MoveInBounds : MonoBehaviour
    {
        public WaypointWithBounds waypoint;
        public enum MovementType { Path, Target, Random }
        [Tooltip("Select the type of movement")] public MovementType movementType;
        [Tooltip("The target transform to move towards when using Target movement type")] public Transform target;
        [Tooltip("Speed at which the object moves")] public float moveSpeed = 2f;
        [Tooltip("Distance threshold to consider the object has arrived at the target")] public float arrivalThreshold = 0.1f;
        [SerializeField] private List<Vector3Int> currentPath = new List<Vector3Int>();
        private int currentPathIndex = 0;
        private float pathRecalcTimer = 0f;
        private const float PATH_RECALC_INTERVAL = 0.5f; // adjust as needed
        private Vector3 _currentTarget;
        private int currentWaypointIndex = 0;

        private void OnEnable()
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (target != null) target.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        private void Update()
        {
  
        }

        public void MoveAlongRandomPath()
        {
            if (waypoint == null) return;
            Vector3 targetPosition = waypoint.GetWorldPosition(_currentTarget);
            if (Vector3.Distance(transform.position, targetPosition) <= arrivalThreshold || !waypoint.GetNode(targetPosition).IsWalkable)
                _currentTarget = new Vector3(Random.Range(0, waypoint.width), Random.Range(0, waypoint.height), Random.Range(0, waypoint.depth));
            else transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

public void MoveAlongPath2()
{
    if (waypoint == null || waypoint.path.Count == 0) return;

    // Get current target waypoint
    Vector3Int targetGridPos = waypoint.path[currentWaypointIndex];
    
    // Get nearest walkable position near that waypoint
    Vector3 targetPosition = waypoint.GetClosestInBounds(waypoint.GetWorldPosition(targetGridPos));

    // Move toward it
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    GetComponent<MumController>().moveDirection = targetPosition - transform.position;


    // Check if reached
    if (Vector3.Distance(transform.position, targetPosition) < arrivalThreshold)
    {
        // Move to next waypoint
        currentWaypointIndex++;
        
        if (currentWaypointIndex >= waypoint.path.Count)
        {
            currentWaypointIndex = waypoint.loop ? 0 : waypoint.path.Count - 1;
        }
    }
}








        private void OnDrawGizmos()
        {
            if (waypoint == null || waypoint.path == null) return;
            switch (movementType)
            {
                case MovementType.Path: PathGizmos(); break;
                case MovementType.Target: TargetGizmos(); break;
                case MovementType.Random: RandomGizmos(); break;
            }

        }
        private void PathGizmos()
        {
            for (int i = 0; i < waypoint.path.Count; i++)
            {
                Vector3 worldPos = waypoint.GetWorldPosition(waypoint.path[i]);
                Gizmos.color = waypoint.IsOutOfBounds(waypoint.path[i]) ? waypoint.blockedNodeColor : waypoint.waypointColor;
                Gizmos.DrawSphere(worldPos, waypoint.nodeGizmozSize + 0.1f);

                if (waypoint.IsOutOfBounds(waypoint.path[i]) || waypoint.path[i] != waypoint.GetClosestInBounds(worldPos))
                {
                    Vector3 closestInBounds = waypoint.GetClosestInBounds(worldPos);
                    Gizmos.color = waypoint.nearestNodeColor;
                    Gizmos.DrawSphere(closestInBounds, waypoint.nodeGizmozSize + 0.1f);
                    Gizmos.color = waypoint.nearestNodeColor;
                    Gizmos.DrawLine(worldPos, closestInBounds);
                }

                if (i < waypoint.path.Count - 1)
                {
                    Vector3 start = waypoint.GetPathLimit(waypoint.path[i]);
                    Vector3 end = waypoint.GetPathLimit(waypoint.path[i + 1]);
                    Gizmos.color = waypoint.pathColor;
                    Gizmos.DrawLine(start, end);
                }
            }

            if (waypoint.loop && waypoint.path.Count > 1)
            {
                Vector3 start = waypoint.GetPathLimit(waypoint.path[0]);
                Vector3 end = waypoint.GetPathLimit(waypoint.path[^1]);
                Gizmos.color = waypoint.pathColor;
                Gizmos.DrawLine(start, end);
            }
        }

        private void TargetGizmos()
        {
            if (target == null) return;
            Gizmos.color = waypoint.pathColor;
            if (waypoint.IsOutOfBounds(target.position) || waypoint.GetNode(target.position).IsWalkable == false)
            {
                Gizmos.DrawLine(transform.position, waypoint.GetClosestInBounds(target.position));
                Gizmos.color = waypoint.nearestNodeColor;
                Gizmos.DrawSphere(waypoint.GetClosestInBounds(target.position), waypoint.nodeGizmozSize + 0.1f);
                Gizmos.color = waypoint.nearestNodeColor;
                Gizmos.DrawLine(waypoint.GetClosestInBounds(target.position), target.position);
            }
            else Gizmos.DrawLine(transform.position, waypoint.GetWorldPosition(waypoint.GetPathLimit(target.position)));
        }
        private void RandomGizmos()
        {
            Gizmos.color = waypoint.waypointColor;
            Gizmos.DrawSphere(waypoint.GetWorldPosition(_currentTarget), waypoint.nodeGizmozSize + 0.1f);
            Gizmos.color = waypoint.pathColor;
            Gizmos.DrawLine(transform.position, waypoint.GetWorldPosition(_currentTarget));
        }






        public void MoveAlongTarget()
        {
            if (waypoint == null || target == null) return;
            Vector3 targetWorldPosition = waypoint.GetWorldPosition(waypoint.GetPathLimit(target.position));
            print(targetWorldPosition);
            print(target.position);
            Vector3 walkTo = new Vector3(target.position.x,0.5f,target.position.z);
            GetComponent<MumController>().moveDirection = walkTo - transform.position ;
            print(waypoint.GetGridPosition(target.position));  
            if (!waypoint.GetNode(waypoint.GetGridPosition(target.position)).IsWalkable) targetWorldPosition = waypoint.FindNearestWalkableNode(targetWorldPosition);
            else if (Vector3.Distance(transform.position, walkTo) > arrivalThreshold) transform.position = Vector3.MoveTowards(transform.position, walkTo, moveSpeed * Time.deltaTime);
            else GetComponent<MumController>().PositionReached();
        }




        
    }   
}
