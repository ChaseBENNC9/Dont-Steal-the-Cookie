using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;
public class CharacterSelection : MonoBehaviour
{
    private bool characterChosen;
    private bool nameChosen;
    [SerializeField] private Button confirmButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ValidateCharacterSelection()
    {
        if(nameChosen && characterChosen)
        {
            confirmButton.interactable = true;
        }
    }
    public void ValidatePlayerName(TMP_InputField input)
    {
        //Validation
        GameSettings.playerName = input.text;
        ES3.Save("PlayerName",GameSettings.playerName);
        nameChosen = true;
    }
    void Start()
    {
        characterChosen = false;
        nameChosen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectCharacter(int selection)
    {
        GameSettings.playerCharacter = (GameSettings.CharacterSelection)selection;
        ES3.Save("SelectedCharacter", GameSettings.playerCharacter);
        characterChosen = true;
    }
}
