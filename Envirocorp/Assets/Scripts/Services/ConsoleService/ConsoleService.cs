using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Utilities;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The Console service is responsible 
    /// for taking input from the user
    /// and sending out commands. Mainly
    /// for debugging and testing.
    /// </summary>
    public class ConsoleService : MonoBehaviour, IConsoleService
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
            if (m_inputService.isRegistered())
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
            AddTextToBackLog(m_inputField.text, true);
            ProcessInput(m_inputField.text);
            m_inputField.ActivateInputField();
            m_inputField.text = "";
        }

        private void AddTextToBackLog(string value, bool command)
        {
            if (string.IsNullOrEmpty(value)) return;
            if (command)
                m_backlogText.text += $"\n>>><b><color=#298E37>{value}<color=#D9D9D9></b>";

            if (!command)
                m_backlogText.text += $"\n{value}\n";

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
            CommandInput input = ParseInput(text);

            if (input.CommandValue.Equals("HELP"))
            {
                ListHelpCommands();
                return;
            }

            if (input.CommandValue.Equals("CLEAR"))
            {
                SetConsoleText();
                return;
            }

            Command command = m_commands.Where(x => x.CommandString.ToUpper().Equals(input.CommandValue)).FirstOrDefault();
            if (command == null)
            {
                AddTextToBackLog("Error : Invalid Command", false);
                return;
            }
            AddTextToBackLog(command.Execute(), false);
        }

        private CommandInput ParseInput(string text)
        {
            text = text.ToUpper();
            text = text.Replace("(", " ( ");
            text = text.Replace(")", " )");

            string commandValue = ParseCommandValue(text);
            string parameterInput = ParseParameters(text);

            return new CommandInput(commandValue, parameterInput);;
        }

        private void ListHelpCommands()
        {
            m_backlogText.text +=
                "\n" +
                "---------------------------------------------\n" +
                "              Command Help List              \n" +
                "\n";

            foreach (Command command in m_commands)
            {
                m_backlogText.text += $"- {command.CommandString}\n" +
                    $"<i>    {command.CommandDefinition}</i>\n\n";
            }

            m_backlogText.text +=
                             "---------------------------------------------\n";
        }
        #endregion

        #region Low Level Functions
        private string ParseParameters(string text)
        {
            if (!text.Contains("(")) return string.Empty;
            return "(" + text.Split(new char[] { '(' }).Last().Replace(" ", String.Empty);
        }

        private string ParseCommandValue(string text)
        {
            string[] input = text.Split(new char[] { ' ', ',' })
                                 .Where(x => x != string.Empty).ToArray(); ;

            string commandValue = String.Join(" ", input.ToArray())
                                        .Split(new char[] { '(' })[0];

            commandValue = commandValue.Trim();
            return commandValue;
        }

        #endregion
    }

    public struct CommandInput
    {
        public string CommandValue;
        public string CommandParameters;

        public CommandInput(string value, string parameters)
        {
            CommandValue = value;
            CommandParameters = parameters;
        }
    }
}
