﻿using UnityEngine;

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
        public HexCoordinate m_Coordinate;
        public Color m_Color;

		private void Awake()
		{
            m_Color = Color.white;
		}
	}
}
