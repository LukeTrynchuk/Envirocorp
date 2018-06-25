using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;
using System;
using FireBullet.Enviro.Utilities;
using System.Linq;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The world generator is responsible for
    /// generating the game board during runtime.
    /// </summary>
    public class RandomWorldGenerator : MonoBehaviour, IWorldGenerator
    {
        #region Public Variables
        public event Action<HexCell[], int, int> OnWorldGenerated;
        #endregion

        #region Private Variables
        [SerializeField]
        private GameObject m_hexPrefab;

        [SerializeField]
        private HexMesh m_hexMesh;

        private ServiceReference<IBoardService> m_boardService = new ServiceReference<IBoardService>();

        private ServiceReference<IHexCoordinateVisualizerService> m_hexCoordinateVisualizer
                                = new ServiceReference<IHexCoordinateVisualizerService>();

        private HexCell[] m_cells;
        private int m_width, m_height;
        #endregion

        #region Main Methods
        void Start() => RegisterService();

        public void RegisterService()
        {
            ServiceLocator.Register<IWorldGenerator>(this);
        }

        public void GenerateWorld(int width, int height)
        {
            InitializeData(width, height);

            GenerateBoard(width, height);

            m_hexMesh.Triangulate(m_cells);

            m_hexCoordinateVisualizer.Reference?.Visualize(m_hexCoordinateVisualizer.Reference.visible);

            OnWorldGenerated?.Invoke(m_cells, m_width, m_height);
        }

        public void RetriangulateWorld()
        {
            if (!m_boardService.isRegistered()) return;
            m_hexMesh.Triangulate(m_boardService.Reference.GetBoard());
        }
        #endregion

        #region Utility Methods		
		private void GenerateBoard(int width, int height)
		{
			for (int i = 0, k = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					CreateCell(i, j, k++);
				}
			}
            GenerateNeighborRelationships();
		}

		private void CreateCell(int i, int j, int v)
		{
            Vector3 position = GenerateHexPosition(i, j);

            CreateHexObject(v, i, j, position);

            m_hexCoordinateVisualizer.Reference?.CreateHexCoordinate(position, i, j, m_cells[v]);
        }

        private void CreateHexObject(int i, int x, int z, Vector3 position)
        {
            HexCell cell = m_cells[i] = Instantiate(m_hexPrefab).GetComponent<HexCell>();
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.m_Coordinate = HexCoordinate.FromOffsetCoordinates(x, z);
        }

        private static Vector3 GenerateHexPosition(int i, int j)
        {
            Vector3 position;
            position.x = (i + j * 0.5f - j / 2) * (HexMetrics.innerRadius * 2f);
			position.z = j * (HexMetrics.outerRadius * 1.5f);
            position.y = 0f;
            return position;
        }

        private void InitializeData(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_cells = new HexCell[width * height];
            m_hexCoordinateVisualizer.Reference?.ClearVisualization();
        }

        private void GenerateNeighborRelationships()
        {
            GenerateEastWestRelationships();
            GenerateNESWRelationships();
            GenerateSENWRelationships();
        }

        private void GenerateEastWestRelationships()
        {
            int zhighest = 0;
            foreach(HexCell cell in m_cells)
            {
                if (cell.m_Coordinate.Z > zhighest)
                    zhighest = cell.m_Coordinate.Z;
            }

            for (int i = 0; i <= zhighest; i++)
            {
                HexCell[] cells = m_cells.Where(x => x.m_Coordinate.Z == i).ToArray();

                for (int j = 0; j < cells.Length; j++)
                {
                    for (int k = 0; k < cells.Length; k++)
                    {
                        if(cells[j].m_Coordinate.Y == cells[k].m_Coordinate.Y + 1)
                        {
                            cells[j].SetNeighbor(HexDirection.E, cells[k]);
                        }
                    }
                }
            }
        }

        private void GenerateNESWRelationships()
        {
            int xhighest = 0;
            int xlowest = 0;
            foreach (HexCell cell in m_cells)
            {
                if (cell.m_Coordinate.X > xhighest)
                    xhighest = cell.m_Coordinate.X;

                if (cell.m_Coordinate.X < xlowest)
                    xlowest = cell.m_Coordinate.X;
            }

            for (int i = xlowest; i <= xhighest; i++)
            {
                HexCell[] cells = m_cells.Where(x => x.m_Coordinate.X == i).ToArray();

                for (int j = 0; j < cells.Length; j++)
                {
                    for (int k = 0; k < cells.Length; k++)
                    {
                        if (cells[j].m_Coordinate.Y == cells[k].m_Coordinate.Y + 1)
                        {
                            cells[j].SetNeighbor(HexDirection.NE, cells[k]);
                        }
                    }
                }
            }
        }

        private void GenerateSENWRelationships()
        {
            int yhighest = 0;
            int ylowest = 0;
            foreach (HexCell cell in m_cells)
            {
                if (cell.m_Coordinate.Y > yhighest)
                    yhighest = cell.m_Coordinate.Y;

                if (cell.m_Coordinate.Y < ylowest)
                    ylowest = cell.m_Coordinate.Y;
            }

            for (int i = ylowest; i <= yhighest; i++)
            {
                HexCell[] cells = m_cells.Where(x => x.m_Coordinate.Y == i).ToArray();

                for (int j = 0; j < cells.Length; j++)
                {
                    for (int k = 0; k < cells.Length; k++)
                    {
                        if (cells[j].m_Coordinate.X == cells[k].m_Coordinate.X + 1)
                        {
                            cells[j].SetNeighbor(HexDirection.NW, cells[k]);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
