using UnityEngine;

namespace FireBullet.Enviro.Board
{
    /// <summary>
    /// The Hex Coordinate struct is meant to 
    /// be used as a data structure to represent
    /// the coordinate value of a single hex.
    /// </summary>
    [System.Serializable]
    public struct HexCoordinate
    {
        public int X { get; private set; }
        public int Z { get; private set; }

        public HexCoordinate(int x, int z)
        {
            X = x;
            Z = z;
        }

        public static HexCoordinate FromOffsetCoordinates(int x, int z) => new HexCoordinate(x - z / 2, z);
        public override string ToString() => $"{X.ToString()},{Z.ToString()}";
        public string ToStringOnSeparateLines() => $"{X.ToString()}\n{Z.ToString()}";
    }
}
