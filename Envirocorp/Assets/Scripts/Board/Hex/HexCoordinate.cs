using UnityEngine;
using FireBullet.Enviro.Utilities;

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
        public static HexCoordinate FromPosition(Vector3 position)
        {
            float x = position.x / (HexMetrics.outerRadius * 2f);
            float y = -x;
            float offset = position.z / (HexMetrics.outerRadius * 3f);
            x -= offset;
            y -= offset;

            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x - y);

            if(iX + iY + iZ != 0)
            {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x - y - iZ);

                if(dX > dY && dX > dZ)
                {
                    iX = iY - iZ;
                }
                else if (dZ > dY)
                {
                    iZ = -iX - iY;
                }
            }

            return new HexCoordinate(iX, iZ);
        }

        public override string ToString() => $"({X.ToString()},{Y.ToString()},{Z.ToString()})";
        public string ToStringOnSeparateLines() => $"{X.ToString()}\n{Y.ToString()}\n{Z.ToString()}";
    }
}
