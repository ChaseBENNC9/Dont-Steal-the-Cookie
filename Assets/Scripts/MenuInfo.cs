using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInfo : MonoBehaviour
{
    public static MenuInfo Instance;
    public string previousScreen;
    public SceneTypes sceneType;
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
        if (previousScreen.Trim() == "" || previousScreen == null)
        {
            previousScreen = "NONE";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
