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
        private HexCell[] m_neighbors;

        [SerializeField]
        private GameObject m_lineRendererPrefab;
        #endregion

        #region Main Methods
        private void Awake()
        {
            m_Color = Color.white;
            m_neighbors = new HexCell[6];
        }

        public HexCell GetNeighbor(HexDirection direction) => m_neighbors[(int)direction];
        public void SetNeighbor(HexDirection direction, HexCell cell) 
        {
			m_neighbors[(int)direction] = cell;
            cell.m_neighbors[(int)direction.Opposite()] = this;
        }

        public void DisplayNeighbors()
        {
            for (int i = 0; i < m_neighbors.Length; i++)
            {
                if (m_neighbors[i] == null) continue;
                GameObject line = Instantiate(m_lineRendererPrefab, transform.position + (Vector3.up * 5f), Quaternion.identity);
                LineRenderer lineRenderer = line.GetComponent<LineRenderer>();

                lineRenderer.SetPositions(new Vector3[]{
                    line.transform.position,
                    m_neighbors[i].gameObject.transform.position
                });

                line.transform.parent = transform;
            }
        }
        #endregion
    }
}
