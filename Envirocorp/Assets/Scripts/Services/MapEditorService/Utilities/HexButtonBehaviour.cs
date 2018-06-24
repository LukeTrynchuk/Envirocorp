using UnityEngine;
using FireBullet.Enviro.Board;
using FireBullet.Core.Services;
using FireBullet.Enviro.Services;

namespace FireBullet.Enviro.MapEditor
{
    /// <summary>
    /// Hex Button behaviour is responsible for 
    /// storing the hex type definition this 
    /// current button represents. When clicked
    /// this button script should change the 
    /// brush of the map editor.
    /// </summary>
    public class HexButtonBehaviour : MonoBehaviour
    {
        #region Private Variables
        private HexTypeDefinition m_hexDefinition;
        private ServiceReference<IMapEditorService> m_mapEditorService 
                            = new ServiceReference<IMapEditorService>();
        #endregion

        #region Main Methods
        public void SetDefinition(HexTypeDefinition definition) => m_hexDefinition = definition;

        public void OnClicked()
        {
            m_mapEditorService.Reference?.SetCurrentHexBrush(m_hexDefinition);
        }
        #endregion
    }
}
