using FireBullet.Enviro.Board;
using UnityEngine;
using FireBullet.Core.Services;
using System;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The Input service is responsible for 
    /// detecting input from the player and
    /// invoking events based on the input 
    /// for other systems in the game to respond.
    /// </summary>
    public class InputService : MonoBehaviour, IInputService
    {
        #region Public Variables
        public event Action<HexCoordinate> OnHexPressed;

        public event Action OnConsoleKeyPressed;
        public event Action OnConsoleCommandKeyPressed;
        #endregion

        #region Private Variables
        private ServiceReference<IBoardService> m_boardService = new ServiceReference<IBoardService>();
        private ServiceReference<IConsoleService> m_consoleService = new ServiceReference<IConsoleService>();
        #endregion

        #region Main Methods
        void Start() => RegisterService();

        void Update()
        {
            if (Input.GetMouseButton(0)) HandleLeftClick();

            if(Input.GetKeyDown(KeyCode.Backslash)) HandleConsoleKeyPressed();

            if (Input.GetKeyDown(KeyCode.Return)) HandleEnterKeyPressed();
        }

        public void RegisterService()
        {
            ServiceLocator.Register<IInputService>(this);
        }
        #endregion

        #region Utility Methods
        private void HandleLeftClick()
        {
            if (!m_boardService.isRegistered()) return;

            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                TouchCell(hit.point);
            }
        }

        void TouchCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            HexCoordinate coordinate = HexCoordinate.FromPosition(position);

            HexCell cell = m_boardService.Reference.GetCellAt(coordinate);

            if(cell != null)
            {
				OnHexPressed?.Invoke(cell.m_Coordinate);            
            }
        }


        private void HandleEnterKeyPressed()
        {
            bool consoleActive = m_consoleService.Reference?.Active ?? false;

            if(consoleActive) 
            {
                OnConsoleCommandKeyPressed?.Invoke();
                return;
            }
        }

        void HandleConsoleKeyPressed() => OnConsoleKeyPressed?.Invoke();
        
        #endregion
    }
}
