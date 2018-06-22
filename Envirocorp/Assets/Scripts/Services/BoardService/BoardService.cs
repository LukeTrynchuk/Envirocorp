using FireBullet.Enviro.Board;
using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The Board Service is responsible for 
    /// keeping reference to a board that other
    /// services and systems can gain access to.
    /// </summary>
    public class BoardService : MonoBehaviour, IBoardService
    {
        #region Public Variables
        public int Width => m_width;
        public int Height => m_height;
        #endregion

        #region Private Variables
        private int m_width = 0;
        private int m_height = 0;
        private HexCell[] m_worldCells;
        private ServiceReference<IWorldGenerator> m_worldGenerator = new ServiceReference<IWorldGenerator>();
        #endregion

        #region Main Methods
        void Start()
        {
            RegisterService();
            m_worldGenerator.AddRegistrationHandle(HandleWorldGeneratorServiceRegistered);
        }

        public HexCell[] GetBoard() => m_worldCells;

        public HexCell GetCellAt(HexCoordinate coordinate)
        {
            foreach(HexCell cell in m_worldCells)
            {
                if(cell.m_Coordinate.X == coordinate.X &&
                  cell.m_Coordinate.Y == coordinate.Y &&
                   cell.m_Coordinate.Z == coordinate.Z)
                {
                    return cell;
                }
            }
            return null;
        }

        public void RegisterService() => ServiceLocator.Register<IBoardService>(this);
        #endregion

        #region Utility Methods
        private void WorldGenerated(HexCell[] cells, int width, int height)
        {
            m_worldCells = cells;
            m_width = width;
            m_height = height;
        }

        private void HandleWorldGeneratorServiceRegistered()
        {
            m_worldGenerator.Reference.OnWorldGenerated -= WorldGenerated;
            m_worldGenerator.Reference.OnWorldGenerated += WorldGenerated;
        }
        #endregion
    }
}
