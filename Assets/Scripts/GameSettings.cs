using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum GameState
{
    MENU,
    IN_GAME,
    PAUSED
}
public static class GameSettings
{
    public static GameState gameState;
    public static string[] goals = {"Steal a Cookie" ,"Get to the Kitchen","HIDE!!","Get back to your Room"};
    public static CharacterSelection playerCharacter;
    public enum CharacterSelection
    {
        MALE,
        FEMALE
    }
    public static string playerName = "Sam";


    public static string FormatMessage(string message)
    {
        return message.Replace("<Player>",playerName).Replace("<PLAYER>",playerName.ToUpper());
    }

    public static float MusicVolume;
    public static float SoundEffectsVolume;
    public static float MasterVolume;
    public static bool Mute;
    public static Sprite PlayerCharacterSprite(CharacterSelection character)
    {
        if (character == CharacterSelection.MALE)
        {
            return Resources.Load<Sprite>("character_profile_player_m");
        }
        else 
        {
            return Resources.Load<Sprite>("character_profile_player_f");
        }
    }

}
