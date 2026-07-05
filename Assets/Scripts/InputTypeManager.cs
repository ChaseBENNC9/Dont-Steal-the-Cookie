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
public class InputTypeManager : MonoBehaviour
{
    public InputTypes currentInputType;
    private void Awake()
    {
       currentInputType = GetInputDevice(GetComponent<PlayerInput>());
       UpdateUI();
    }
    public void OnInputDeviceChange(PlayerInput playerInput)
    {
        currentInputType = GetInputDevice(playerInput);
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
        print("HI");
        Cursor.visible = currentInputType == InputTypes.KEYBOARD;
        MenuTooltipIcon[] icons = GameObject.FindObjectsByType<MenuTooltipIcon>();
        print(icons.Length);
        foreach (MenuTooltipIcon icon in icons)
        {
            icon.UpdateUI(currentInputType);
        }
    }
}
