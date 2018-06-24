using UnityEngine;

namespace FireBullet.Enviro.Board
{
    /// <summary>
    /// A hex cell represents a single unit on 
    /// the board. A hex cell contains data about
    /// what kind of resources can be found on the
    /// hex.
    /// </summary>
    public class HexCell : MonoBehaviour
    {
        #region Public Variables
        public HexCoordinate m_Coordinate;
        public Color m_Color;
        #endregion

        #region Private Variables
        [SerializeField]
        private HexCell[] m_neighbours;
        #endregion

        #region Main Methods
        private void Awake()
        {
            m_Color = Color.white;
        }

        public HexCell GetNeighbour(HexDirection direction) => m_neighbours[(int)direction];
        public void SetNeighbour(HexDirection direction, HexCell cell) 
        {
			m_neighbours[(int)direction] = cell;
            cell.m_neighbours[(int)direction.Opposite()] = this;
        }
        #endregion
    }
}
