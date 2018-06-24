using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Utilities;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

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
        private TMP_Text m_backlogText;

        [SerializeField]
        private TMP_InputField m_inputField;

        [SerializeField]
        private ScrollRect m_scrollView;

        [SerializeField]
        private DynamicBoolEvent m_onConsoleActiveStatusChanged;

        [SerializeField]
        private Command[] m_commands;

        private ServiceReference<IInputService> m_inputService = new ServiceReference<IInputService>();
        private bool m_active = false;
        #endregion

        #region Main Methods
        void Start() => SetConsoleText();

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
            m_inputField.ActivateInputField();
            m_inputField.text = "";
            m_onConsoleActiveStatusChanged?.Invoke(m_active);
        }

        void HandleConsoleEnterKeyPressed()
        {
            AddTextToBackLog();
			ProcessInput(m_inputField.text);
			m_inputField.ActivateInputField();
            m_inputField.text = "";
        }

        private void AddTextToBackLog()
        {
            string value = m_inputField.text;
            m_backlogText.text += $"\n>>><b><color=#298E37>{value}<color=#D9D9D9></b>\n";
			m_scrollView.verticalNormalizedPosition = 0f;
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

        private void SetConsoleText()
        {
            m_backlogText.text =
                "Envirocorp Console - FireBullet Games 2018 (C) \n" +
                "---------------------------------------------\n" +
                "Type help for list of all commands.\n";
        }

        private void ProcessInput(string text)
        {
            text = text.ToUpper();

            if(text.Equals("HELP"))
            {
                ListHelpCommands();
                return;
            }

            Command command = m_commands.Where(x => x.CommandString.ToUpper().Equals(text)).FirstOrDefault();
            if(command == null) 
            {
				m_backlogText.text += "Error : Invalid Command\n";
                return;
            }
            command.Execute();
        }

        private void ListHelpCommands()
        {
            m_backlogText.text +=
                "\n" +
                "---------------------------------------------\n" +
                "              Command Help List              \n" +
                "\n";

            foreach(Command command in m_commands)
            {
                m_backlogText.text += $"- {command.CommandString}\n" +
                    $"<i>    {command.CommandDefinition}</i>\n\n";
            }
        }
        #endregion
    }
}
