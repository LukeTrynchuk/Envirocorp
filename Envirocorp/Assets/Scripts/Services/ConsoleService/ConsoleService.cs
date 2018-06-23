using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Utilities;
using TMPro;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The Console service is responsible 
    /// for taking input from the user
    /// and sending out commands. Mainly
    /// for debugging and testing.
    /// </summary>
    public class ConsoleService : MonoBehaviour , IConsoleService
    {
        #region Public Variables
        public bool Active => m_active;
        #endregion

        #region Private Variables
        [SerializeField]
        private TMP_Text m_consoleText;

        [SerializeField]
        private TMP_InputField m_inputField;

        [SerializeField]
        private DynamicBoolEvent m_onConsoleActiveStatusChanged;

        private ServiceReference<IInputService> m_inputService = new ServiceReference<IInputService>();
        private bool m_active = false;
        #endregion

        #region Main Methods
        private void OnEnable()
        {
            RegisterService();
            m_inputService.AddRegistrationHandle(HandleInputServiceRegistered);
        }

        void OnDisable()
        {
            ServiceLocator.Unregister<IConsoleService>(this);
            if(m_inputService.isRegistered())
            {
                m_inputService.Reference.OnConsoleKeyPressed -= HandleConsoleKeyPressed;
            }
        }

		public void RegisterService()
		{
            ServiceLocator.Register<IConsoleService>(this);
		}

        void HandleConsoleKeyPressed()
        {
            m_active = !m_active;
            m_onConsoleActiveStatusChanged?.Invoke(m_active);
        }

        void HandleConsoleEnterKeyPressed()
        {
			m_inputField.ActivateInputField();
            m_inputField.text = "";
        }
        #endregion

        #region Utility Methods
        void HandleInputServiceRegistered()
        {
            m_inputService.Reference.OnConsoleKeyPressed -= HandleConsoleKeyPressed;
            m_inputService.Reference.OnConsoleKeyPressed += HandleConsoleKeyPressed;

            m_inputService.Reference.OnConsoleCommandKeyPressed -= HandleConsoleEnterKeyPressed;
            m_inputService.Reference.OnConsoleCommandKeyPressed += HandleConsoleEnterKeyPressed;
        }
        #endregion
    }
}
