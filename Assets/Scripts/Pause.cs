using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        if (value.performed)
        {
            if (gamePaused)
            {
;
                ResumeGame();
            }
            else
            {
;

                PauseGame();
            }

;
        }

    }

    private void PauseGame()
    {
        gamePaused = true;
        gameObject.SetActive(gamePaused);
        GetComponent<MenuButtons>().SelectButton(transform.Find("Panel").transform.Find("Resume").GetComponent<Button>());
        GameSettings.gameState = GameState.PAUSED;
        GameObject.Find("Player").GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        Time.timeScale = 0;
        Cursor.visible = InputTypeManager.Instance.currentInputType == InputTypes.KEYBOARD;


    }
    public void ResumeGame()
    {

        gamePaused = false;
        Cursor.visible = false;
        GameSettings.gameState = GameState.IN_GAME;
        GameObject.Find("Player").GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        Time.timeScale = 1;
        gameObject.SetActive(gamePaused);
    }
}
