using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
    public enum InputTypes
    {
        TOUCH,
        GAMEPAD,
        KEYBOARD,
        UNKNOWN
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
       UpdateUI();
       currentInputType = GetInputDevice(GetComponent<PlayerInput>());
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
        print("CHANGED DEVICE!!");
        print(currentInputType);
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
        Cursor.visible = currentInputType == InputTypes.KEYBOARD;
        MenuTooltipIcon[] icons = GameObject.FindObjectsByType<MenuTooltipIcon>();
        foreach (MenuTooltipIcon icon in icons)
        {
            icon.UpdateUI(currentInputType);
        }
    }
}
