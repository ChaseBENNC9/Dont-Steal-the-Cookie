using UnityEngine;
using System.Collections.Generic;
public enum MumStates 
{
    NEUTRAL,
    SEARCHING,
    PLAYER_FOUND
}
public class PathfinderController : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMesh;
    public List<Transform> targets;
    [SerializeField] private Vector3 currentGoal;
    [SerializeField] private MumStates currentState;
    public bool ready;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = MumStates.NEUTRAL;
        navMesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == MumStates.NEUTRAL)
        {

            if(navMesh.remainingDistance < 0.1f && !ready)
            {
                ready = true;
                IteratePositions(targets);
            }
        else if (ready)
            {
                print("YES");
                GoTo(targets[0]);

            }
        }
        else if (currentState == MumStates.PLAYER_FOUND)
        {
            navMesh.speed *= 5;
            navMesh.destination = GameObject.Find("Player").transform.position;

        }
        
    }
    private void GoTo(Transform goal)
    {
            if(ready)
            {
                ready = false;
                if (navMesh.destination == goal.position)
                    return;
                navMesh.destination = goal.position;
            }
        
        
    }
    private void IteratePositions(List<Transform> targets)
    {
        targets.Insert(targets.Count-1,PopFirstTarget(targets));
    }
    private Transform PopFirstTarget(List<Transform> targets)
    {
        Transform first = targets[0];
        targets.Remove(first);
        return first;
    }

    public void SwitchState(MumStates state)
    {
        currentState = state;
    }
}
