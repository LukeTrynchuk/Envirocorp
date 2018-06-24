﻿using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Utilities;
using TMPro;
using System;
using UnityEngine.UI;

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
			m_inputField.ActivateInputField();
            m_inputField.text = "";
        }

        private void AddTextToBackLog()
        {
            string value = m_inputField.text;
            m_backlogText.text += $"\n>>><b><color=#298E37>{value}<color=#D9D9D9><b>\n";
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
        #endregion
    }
}
