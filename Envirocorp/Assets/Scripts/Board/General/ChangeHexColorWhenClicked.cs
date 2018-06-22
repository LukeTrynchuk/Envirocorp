using UnityEngine;
using FireBullet.Enviro.Services;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Board
{
    /// <summary>
    /// Change hex color when clicked will change the 
    /// color of a clicked hex when the user clicks on 
    /// the hex. 
    /// </summary>
    public class ChangeHexColorWhenClicked : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private Color m_changeColor;

        private ServiceReference<IBoardService> m_boardService = new ServiceReference<IBoardService>();
        private ServiceReference<IInputService> m_inputService = new ServiceReference<IInputService>();
        private ServiceReference<IWorldGenerator> m_worldGenerator = new ServiceReference<IWorldGenerator>();
        #endregion

        #region Main Methods
        private void OnEnable()
        {
            m_inputService.AddRegistrationHandle(HandleInputServiceRegistered);
        }

		private void OnDisable()
		{
			if(m_inputService.isRegistered())
            {
                m_inputService.Reference.OnHexPressed -= UserClickedHex;
            }
		}

		private void UserClickedHex(HexCoordinate coordinate)
        {
            if (!m_boardService.isRegistered()) return;
            if (!m_worldGenerator.isRegistered()) return;

            HexCell cell = m_boardService.Reference.GetCellAt(coordinate);
            cell.m_Color = m_changeColor;

            m_worldGenerator.Reference.RetriangulateWorld();
        }
        #endregion

        #region Utility Methods
        private void HandleInputServiceRegistered()
        {
            m_inputService.Reference.OnHexPressed -= UserClickedHex;
            m_inputService.Reference.OnHexPressed += UserClickedHex;
        }
        #endregion
    }
}
