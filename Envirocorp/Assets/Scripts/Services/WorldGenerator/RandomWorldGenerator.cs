﻿using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;
using UnityEngine.UI;
using System;
using FireBullet.Enviro.Utilities;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The world generator is responsible for
    /// generating the game board during runtime.
    /// </summary>
    public class RandomWorldGenerator : MonoBehaviour, IWorldGenerator
    {
        #region Public Variables
        public event System.Action<HexCell[], int, int> OnWorldGenerated;
        #endregion

        #region Private Variables
        [SerializeField]
        private GameObject m_hexPrefab;

        [SerializeField]
        private Text m_cellLabelPrefab;

        [SerializeField]
        private Canvas m_gridCanvas;

        [SerializeField]
        private HexMesh m_hexMesh;

        [SerializeField]
        private Color defaultColor = Color.white;

        [SerializeField]
        private Color touchedColor = Color.magenta;

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
            m_width = width;
            m_height = height;

            m_cells = new HexCell[width * height];
            GenerateBoard(width, height);
            m_hexMesh.Triangulate(m_cells);

            OnWorldGenerated?.Invoke(m_cells, m_width, m_height);
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
		}
		
		private void CreateCell(int i, int j, int v)
		{
            Vector3 position = GenerateHexPosition(i, j);

            CreateHexObject(v, i, j, position);

            CreateCellLabel(position, i,j, v);
        }

        private void CreateHexObject(int i, int x, int z, Vector3 position)
        {
            HexCell cell = m_cells[i] = Instantiate(m_hexPrefab).GetComponent<HexCell>();
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.m_Coordinate = HexCoordinate.FromOffsetCoordinates(x, z);
            cell.m_Color = defaultColor;
        }

        private static Vector3 GenerateHexPosition(int i, int j)
        {
            Vector3 position;
            position.x = (i + j * 0.5f - j / 2) * (HexMetrics.innerRadius * 2f);
			position.z = j * (HexMetrics.outerRadius * 1.5f);
            position.y = 0f;
            return position;
        }

        private void CreateCellLabel(Vector3 position, int x, int z, int index)
        {
            Text label = Instantiate<Text>(m_cellLabelPrefab);
            label.rectTransform.SetParent(m_gridCanvas.transform, false);
            label.rectTransform.anchoredPosition =
                new Vector2(position.x, position.z);
            label.text = m_cells[index].m_Coordinate.ToStringOnSeparateLines();
        }
        #endregion
    }
}
