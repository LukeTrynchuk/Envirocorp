using FireBullet.Enviro.Board;
using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Core.Services
{
    /// <summary>
    /// Hex Definition service is responsible for 
    /// providing all hex definitions to other services
    /// and systems.
    /// </summary>
    public class HexDefinitionService : MonoBehaviour, IHexDefinitionService
    {
        #region Private Variables
        [SerializeField]
        HexTypeDefinition[] m_definitions;
        #endregion

        #region Main Methods
        public HexTypeDefinition[] GetHexDefinitions() => m_definitions;

        void Start() => RegisterService();

        public void RegisterService()
        {
            ServiceLocator.Register<IHexDefinitionService>(this);
        }
        #endregion
    }
}
