using UnityEngine;

public static class MarcoPoloGame
{
    public const int ROUNDS_PER_GAME = 4;
    public const int PLAYERS_IN_MATCH = 2;
    public const int ROUND_TIME = 60;

    public const int PRE_ROUND_TIME = 10;

    public const string IS_HUNTER = "isHunter";
    public const string IS_ALIVE = "isAlive";

    public const string ROUNDS_PROGRESS = "roundsProgress";

    #region PLAYER SETTINGS
    public const float LIGHT_INT_NOT_PLAYING = 1.0f;
    public const float LIGHT_INT_HUNTED = 4.0f;
    public const float LIGHT_INT_HUNTER = 0.0f;

    #endregion

    #region GAME SETTINGS
    public const float PROJECTILE_PULSE_DUR = 3.0f;
    public const float PROJECTILE_SPD = 5.0f;

    public const float ECHO_DELAY = 1.0f;
    public const float DASH_CD = 2.0f;

    #endregion
}