using UnityEngine;
using FireBullet.Enviro.Board;
using FireBullet.Core.Services;
using FireBullet.Enviro.Services;
using FireBullet.Enviro.Utilities;

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
        [SerializeField]
        private DynamicBoolEvent m_onSelectedStateChanged;

        private HexTypeDefinition m_hexDefinition;
        private ServiceReference<IMapEditorService> m_mapEditorService
                            = new ServiceReference<IMapEditorService>();
        #endregion

        #region Main Methods
        public void SetDefinition(HexTypeDefinition definition) => m_hexDefinition = definition;

        private void OnEnable()
        {
            m_mapEditorService.AddRegistrationHandle(HandleMapEditorServiceRegistered);
        }

        private void OnDisable()
        {
            if(m_mapEditorService.isRegistered())
            {
                m_mapEditorService.Reference.OnBrushChanged -= NewBrushSelected;
            }
        }

        private void NewBrushSelected(HexTypeDefinition definition) => 
                m_onSelectedStateChanged?.Invoke(definition == m_hexDefinition);

        public void OnClicked()
        {
            m_mapEditorService.Reference?.SetCurrentHexBrush(m_hexDefinition);
        }
        #endregion

        #region Utility Methods
        private void HandleMapEditorServiceRegistered()
        {
            m_mapEditorService.Reference.OnBrushChanged -= NewBrushSelected;
            m_mapEditorService.Reference.OnBrushChanged += NewBrushSelected;
        }
        #endregion
    }
}
