using System.Collections;
using System.Collections.Generic;
using FireBullet.Enviro.Board;
using UnityEngine;

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
        public event System.Action<HexCoordinate> OnHexPressed;
        #endregion

        #region Main Methods
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                HandleLeftClick();
            }
        }
        #endregion

        #region Utility Methods
        private void HandleLeftClick()
        {
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

            int index = coordinate.X + coordinate.Z * m_width + coordinate.Z / 2;
            HexCell cell = m_cells[index];
            cell.m_Color = touchedColor;
            m_hexMesh.Triangulate(m_cells);

            Debug.Log($"Touched at {coordinate.ToString()}");
        }
        #endregion
    }
}
