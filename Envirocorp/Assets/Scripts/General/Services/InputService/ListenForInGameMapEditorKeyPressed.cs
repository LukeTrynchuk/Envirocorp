using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Services;
using FireBullet.Enviro.Utilities;

namespace FireBullet.Enviro.General
{
    /// <summary>
    /// Listen for in game map editor key pressed will listen 
    /// for when the in game map editor key was pressed and will
    /// invoke a unity event for other objects to subscribe to.
    /// </summary>
    public class ListenForInGameMapEditorKeyPressed : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private DynamicBoolEvent m_onEditorKeyPressed;

        private ServiceReference<IInputService> m_inputService = new ServiceReference<IInputService>();
        #endregion

        #region Main Methods
        private void OnEnable()
        {
            m_inputService.AddRegistrationHandle(HandleInputServiceRegistered);
        }

        private void OnDisable()
        {
            if(m_inputService.isRegistered())
            {
                m_inputService.Reference.OnInGameMapEditorRequested -= OnMapEditorKeyPressed;
            }
        }

        private void OnMapEditorKeyPressed(bool value) => m_onEditorKeyPressed?.Invoke(value);
        #endregion

        #region Utility Methods
        private void HandleInputServiceRegistered()
        {
            m_inputService.Reference.OnInGameMapEditorRequested -= OnMapEditorKeyPressed;
            m_inputService.Reference.OnInGameMapEditorRequested += OnMapEditorKeyPressed;
        }
        #endregion
    }
}
