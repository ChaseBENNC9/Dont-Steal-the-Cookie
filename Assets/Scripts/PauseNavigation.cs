using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class PauseNavigation : MonoBehaviour
{
    private void OnEnable()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent eventsLoadingOptions;
    public UnityEvent eventsUnloadingOptions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LeaveOptions()
    {
        {
            if (SceneManager.GetActiveScene().name == "House")
            {
                ExitOptions();        // Your original method
            }
        }
    }


    public void LoadOptions()
    {   
        eventsLoadingOptions.Invoke();
    }
    public void ExitOptions()
    {
        eventsUnloadingOptions.Invoke();   
    }
}
