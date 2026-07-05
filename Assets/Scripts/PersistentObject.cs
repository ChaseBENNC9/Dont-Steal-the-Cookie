using UnityEngine;
public class PersistentObject : MonoBehaviour
{
    public static PersistentObject Instance { get; private set; }

    private void Awake()
    {
        // Check if an instance already exists in the scene
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate
            return;
        }

        // Set the active instance and protect it from destruction
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}