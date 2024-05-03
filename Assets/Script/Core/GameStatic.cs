using UnityEngine;
public static class GameStatic
{
    public static bool IS_ANIMATING = false;
    #region game property
    public readonly static int ARMOR_COEFFICIENT = 500;
    public readonly static float GUN_MAX_SPREAD_ANGLE = 60;
    #endregion

    #region color
    public readonly static Color CRITICAL_TIER_0_COLOR = Color.white;
    public readonly static Color CRITICAL_TIER_1_COLOR = Color.yellow;
    public readonly static Color CRITICAL_TIER_2_COLOR = new Color(1f, 0.5f, 0f, 1f);
    public readonly static Color CRITICAL_TIER_3_COLOR = Color.red;
    public readonly static Color CRITICAL_TIER_4_COLOR = Color.red;
    public readonly static Color CRITICAL_TIER_5_COLOR = Color.red;
    public readonly static Color USER_UI_COLOR_BLUE = Color.blue;
    public readonly static Color USER_UI_COLOR_CYAN = Color.cyan;
    #endregion

    #region API
    public readonly static string HOST = "https://localhost:7121";
    public readonly static string GET_MUTATION_API = "/api/Mutations";
    public readonly static string GET_ENEMY_API = "/api/EnemyCells";
    public readonly static string GET_ABILITY_API = "/api/MutationAbilities";
    #endregion
}