using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectCharacter(int selection)
    {
        GameSettings.playerCharacter = (GameSettings.CharacterSelection)selection;
        ES3.Save("SelectedCharacter", GameSettings.playerCharacter);
    }
}
