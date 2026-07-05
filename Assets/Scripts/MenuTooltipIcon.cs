using UnityEngine;
using UnityEngine.UI;

public class MenuTooltipIcon : MonoBehaviour
{

    public Sprite keyboardIcon;
    public Sprite gamepadIcon;
    public Image image;
    private Sprite current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        UpdateUI(InputTypeManager.Instance.currentInputType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateUI(InputTypes inputType)
    {
        switch (inputType)
        {
            case InputTypes.KEYBOARD:
                current = keyboardIcon;
                break;
            case InputTypes.GAMEPAD:
                current = gamepadIcon;
                break;
        }
        image.GetComponent<Image>().sprite = current;
    }
}
