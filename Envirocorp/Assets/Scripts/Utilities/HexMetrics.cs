using UnityEngine;

namespace FireBullet.Enviro.Utilities
{
    /// <summary>
    /// Hex metrics are const values that
    /// can be accessed by any system anywhere
    /// inside the game.
    /// </summary>
    public static class HexMetrics
    {
        #region Public Variables
        public const float outerRadius = 10f;
        public const float innerRadius = outerRadius * 0.866025404f;

        public static Vector3[] corners = {
            new Vector3(0f, 0f, outerRadius),
            new Vector3(innerRadius, 0f, 0.5f * outerRadius),
            new Vector3(innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(0f,0f, -outerRadius),
            new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
            new Vector3(0f,0f,outerRadius)
        };
        #endregion
    }
}
