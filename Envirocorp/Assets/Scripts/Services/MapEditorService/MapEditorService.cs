﻿using UnityEngine;
using FireBullet.Enviro.Utilities;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// A Map Editor is responsible for editing
    /// the map during runtime. 
    /// 
    /// The Purpose of the map editor is to 
    /// help with testing and creating custom maps.
    /// </summary>
    public class MapEditorService : MonoBehaviour, IMapEditorService
    {
        #region Private Variables
        [SerializeField]
        private DynamicBoolEvent m_onActivatedStateChanged;

        [SerializeField]
        private Color m_changeColor;

        private ServiceReference<IInputService> m_inputService = new ServiceReference<IInputService>();
        private ServiceReference<IBoardService> m_boardService = new ServiceReference<IBoardService>();
        private ServiceReference<IWorldGenerator> m_worldGenerator = new ServiceReference<IWorldGenerator>();

        private bool m_activated = false;
        #endregion

        #region Main Methods
        void Start() => RegisterService();

        void OnEnable()
        {
            m_inputService.AddRegistrationHandle(HandleInputServiceRegistered);
        }

        void OnDisable()
        {
            if(m_inputService.isRegistered())
            {
                m_inputService.Reference.OnHexPressed -= HandleUserClickedHex;
            }
        }

        public void Activate(bool value)
        {
            m_activated = value;
            m_onActivatedStateChanged?.Invoke(m_activated);
        }

        public void RegisterService()
        {
            ServiceLocator.Register<IMapEditorService>(this);
        }

        void HandleUserClickedHex(HexCoordinate coordinate)
        {
            if (!m_activated) return;

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
            m_inputService.Reference.OnHexPressed -= HandleUserClickedHex;
            m_inputService.Reference.OnHexPressed += HandleUserClickedHex;
        }
        #endregion
    }
}
