using UnityEngine;

public static class Globals
{
    public enum LastColor { RED, BLUE };
    public enum GameplayState { PlayerTurn, BotTurn };

    public static GameplayState gpState = GameplayState.PlayerTurn;
    public static LastColor lastColor = LastColor.RED;
    public static float heightRed, heightBlue, widthRed, widthBlue = 0, sizeBlue, sizeRed;

    //Signals
    public static bool canCombineMeshes = false;
    public static bool canUpdateSurface = false;
    public static bool botCanCut = false;
}