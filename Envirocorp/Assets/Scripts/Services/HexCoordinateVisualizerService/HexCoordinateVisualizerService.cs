using UnityEngine;
using FireBullet.Core.Services;
using System.Collections.Generic;
using System.Collections;
using FireBullet.Enviro.Board;
using UnityEngine.UI;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// Hex coordinate visualizer service is
    /// responsible for visualizing the coordinates
    /// of the hex map in the game view.
    /// </summary>
    public class HexCoordinateVisualizerService : MonoBehaviour, IHexCoordinateVisualizerService
    {
        #region Public Variables
        public bool visible => m_visible;
        #endregion

        #region Private Variables
        [SerializeField]
        private Text m_labelPrefab;

        [SerializeField]
        private Canvas m_gridCanvas;

        List<GameObject> m_hexLabelsList = new List<GameObject>();
        private bool m_visible = false;
        #endregion

        #region Main Methods
        void Start() => RegisterService();

        public void RegisterService()
        {
            ServiceLocator.Register<IHexCoordinateVisualizerService>(this);
        }

        public void Visualize(bool value)
        {
            m_visible = value;
            foreach (GameObject hexCoordinateLabel in m_hexLabelsList) 
                        hexCoordinateLabel.SetActive(m_visible);
        }

        public void CreateHexCoordinate(Vector3 position, int x, int y, HexCell cell)
        {
            Text label = Instantiate<Text>(m_labelPrefab);
            label.rectTransform.SetParent(m_gridCanvas.transform, false);
            label.rectTransform.anchoredPosition =
                new Vector2(position.x, position.z);
            label.text = cell.m_Coordinate.ToStringOnSeparateLines();
            m_hexLabelsList.Add(label.gameObject);
        }

        public void ClearVisualization()
        {
            for (int i = m_hexLabelsList.Count - 1; i >= 0; i--)
            {
                Destroy(m_hexLabelsList[i]);
            }

            m_hexLabelsList.Clear();
        }
        #endregion
    }
}
