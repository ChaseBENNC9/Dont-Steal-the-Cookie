using UnityEngine;
using System.Collections;
public enum GameState
{
    MENU,
    IN_GAME,
    PAUSED
}
public static class GameSettings
{
    public static GameState gameState;
    public static InputTypes gameInputType;
    public static string[] goals = {"Steal a Cookie" ,"Get to the Kitchen","HIDE!!","Get back to your Room"};

}
