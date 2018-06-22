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
        #region Public Variables
        public int X => m_x;
		public int Y => -X - Z;
        public int Z => m_z;
        #endregion

        #region Private Variables
        [SerializeField]
        private int m_x;

        [SerializeField]
        private int m_z;
        #endregion

        public HexCoordinate(int x, int z)
        {
            this.m_x = x;
            this.m_z = z;
        }

        public static HexCoordinate FromOffsetCoordinates(int x, int z) => new HexCoordinate(x - z / 2, z);
        public override string ToString() => $"({X.ToString()},{Y.ToString()},{Z.ToString()})";
        public string ToStringOnSeparateLines() => $"{X.ToString()}\n{Y.ToString()}\n{Z.ToString()}";
    }
}
