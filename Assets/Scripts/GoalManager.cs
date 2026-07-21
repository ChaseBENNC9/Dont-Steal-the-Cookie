using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public static GoalManager Instance;
    private Goal currentGoal;
    private Goal subGoal;

    public Transform goalContainer;
    public GameObject goalPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate
            return;
        }
        Instance = this;

    }
     void Start()
    {
;
        GameObject g = GameObject.Instantiate(goalPrefab,goalContainer);
        currentGoal = g.GetComponent<Goal>();
        currentGoal.Init(GameSettings.goals[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerGoal(string goal)
    {
        if (currentGoal.GetName() == goal)
            currentGoal.Complete();
    }

    public void UpdateGoal(string goal)
    {

    }
    public void UpdateSubGoal(string goal)
    {
        if (subGoal)
        {
            if (subGoal.GetName() != goal)
            {
                GameObject g = GameObject.Instantiate(goalPrefab,goalContainer);
                subGoal = g.GetComponent<Goal>();
                subGoal.Init(goal);
            }
        }
        else
        {
            GameObject g = GameObject.Instantiate(goalPrefab,goalContainer);
            subGoal = g.GetComponent<Goal>();
            subGoal.Init(goal);
        }
    }

}
