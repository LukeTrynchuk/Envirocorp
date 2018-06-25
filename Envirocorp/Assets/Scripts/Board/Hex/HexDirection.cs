using UnityEngine;

namespace FireBullet.Enviro.Board
{
    /// <summary>
    /// The Hex Direction enum should be
    /// used to represent a direction on the
    /// hex board. Each hex has 6 neighbours and
    /// therefore there are 6 directions.
    /// </summary>
    public enum HexDirection 
    {
        NE,
        E,
        SE,
        SW,
        W,
        NW
    }

    public static class HexDirectionExtensions
    {
        public static HexDirection Opposite (this HexDirection direction)
        {
            return (int)direction < 3 ? (direction + 3) : (direction - 3);
        }
    }
}
