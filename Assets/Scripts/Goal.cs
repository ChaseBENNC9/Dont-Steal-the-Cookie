using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    private string goalName;
    private TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(string goal)
    {
        goalName = goal;

    }
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = goalName;
        text.color = new Color(0,0,0);

        
    }
    public string GetName()
    {
        return goalName;
    }

    private void Update()
    {

    }
    // Update is called once per frame
    public void Complete()
    {
        text.color = new Color(0,255,0);
    }
}
