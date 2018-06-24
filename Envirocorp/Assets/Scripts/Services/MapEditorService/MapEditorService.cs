using UnityEngine;
using FireBullet.Enviro.Utilities;
using FireBullet.Core.Services;

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

        private bool m_activated = false;
        #endregion

        #region Main Methods
        void Start() => RegisterService();

        public void Activate(bool value)
        {
            m_activated = value;
            m_onActivatedStateChanged?.Invoke(m_activated);
        }

        public void RegisterService()
        {
            ServiceLocator.Register<IMapEditorService>(this);
        }
        #endregion
    }
}
