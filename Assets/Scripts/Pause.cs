using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public bool gamePaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePaused = false;
        gameObject.SetActive(gamePaused);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPause(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            gamePaused = !gamePaused;
            gameObject.SetActive(gamePaused);
        }
    }

    public void ResumeGame()
    {
        gamePaused = false;
        gameObject.SetActive(gamePaused);

    }
}
