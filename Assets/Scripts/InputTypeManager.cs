using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Data.Common;
public enum InputTypes
    {
        UNKNOWN,
        TOUCH,
        GAMEPAD,
        KEYBOARD
        
    }
    public enum SceneTypes
{
    MENU,
    GAME
}
public class InputTypeManager : MonoBehaviour
{
    public InputTypes currentInputType;
    public static InputTypeManager Instance;
    private void Awake()
    {
;
       if (TryGetComponent<PlayerInput>(out PlayerInput input))
        {   
            currentInputType = GetInputDevice(input);
        }
        else
        {
            currentInputType = GetInputDevice(GameObject.Find("Player").GetComponent<PlayerInput>());
        }
        
       UpdateUI();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate
            return;
        }

        // Set the active instance and protect it from destruction
        Instance = this;
    }
    public void OnInputDeviceChange(PlayerInput playerInput)
    {
        currentInputType = GetInputDevice(playerInput);
;
;
        UpdateUI();
    }


    private InputTypes GetInputDevice(PlayerInput playerInput)
    {
        return (object)playerInput.currentControlScheme switch
        {
            "Keyboard&Mouse" => InputTypes.KEYBOARD,    
            "Gamepad" => InputTypes.GAMEPAD,
            "Touch" => InputTypes.TOUCH,
            _ => InputTypes.UNKNOWN,
        };
    }

    private void UpdateUI()
    {
        if(GameSettings.gameState == GameState.MENU || GameSettings.gameState == GameState.PAUSED)
        {
            // Cursor.visible = currentInputType == InputTypes.KEYBOARD;
            MenuTooltipIcon[] icons = GameObject.FindObjectsByType<MenuTooltipIcon>();
            foreach (MenuTooltipIcon icon in icons)
            {
                icon.UpdateUI(currentInputType);
            }       
        }

    }
}
