using UnityEngine;

public class GoalProximityTrigger : MonoBehaviour
{
    public string goalTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GoalManager.Instance.UpdateSubGoal(goalTrigger);
        }
    }   
    
}
