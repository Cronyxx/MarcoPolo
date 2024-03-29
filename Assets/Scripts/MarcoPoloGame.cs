using UnityEngine;

public static class MarcoPoloGame
{
    public const int ROUNDS_PER_GAME = 4;
    public const int PLAYERS_IN_MATCH = 2;
    public const int ROUND_TIME = 100;

    public const int PRE_ROUND_TIME = 10;

    public const string IS_HUNTER = "isHunter";
    public const string IS_ALIVE = "isAlive";

    public const string ROUNDS_PROGRESS = "roundsProgress";


    #region PLAYER SETTINGS
    public const float LIGHT_INT_NOT_PLAYING = 1.0f;
    public const float LIGHT_INT_HUNTED = 4.0f;
    public const float LIGHT_INT_HUNTER = 0.0f;

    public const float PLAYER_SPEED = 10.0f;

    #endregion

    #region GAME SETTINGS

    public const float PLAY_AREA_WIDTH = 20.0f;
    public const float PLAY_AREA_HEIGHT = 20.0f;
    public const float PROJECTILE_PULSE_DUR = 3.0f;
    public const float PROJECTILE_SPD = 5.0f;

    public const float ECHO_DELAY = 1.0f;
    public const float HUNTER_REVEAL_CD = 3.0f;
    public const int SKILL_COUNT = 5;

    #endregion

    #region SKILL SETTINGS
    public const int SKILL_TYPE_GLOBAL = -1;
    public const int SKILL_TYPE_HUNTER = 0;
    public const int SKILL_TYPE_HUNTED = 1;
    public const float SKILL_SPAWN_INTERVAL = 1.5f;
    public const float SKILL_FREEZE_DUR = 2.5f;
    public const float SKILL_SLOW_DUR = 3.0f;
    public const float SKILL_SLOW_SPD = 5.0f;

    public const float SKILL_FAST_DUR = 3.0f;
    public const float SKILL_FAST_SPD = 15.0f;

    public const float SKILL_ECHO_DUR = 5.0f;

    #endregion
}