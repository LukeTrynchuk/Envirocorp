using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Services;
using FireBullet.Enviro.Board;
using UnityEngine.UI;
using FireBullet.Enviro.MapEditor;
using System.Collections;
using System.Collections.Generic;

namespace FireBullet.Enviro.UI
{
    /// <summary>
    /// Creates the required number of hex type buttons
    /// for use in the Map Editor UI.
    /// </summary>
    public class CreateHexTypeButtons : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private GameObject m_hexButtonPrefab;

        private ServiceReference<IHexDefinitionService> m_hexDefinitionService
                                = new ServiceReference<IHexDefinitionService>();
        #endregion

        #region Main Methods
        private void Start()
        {
            m_hexDefinitionService.AddRegistrationHandle(HandleHexDefinitionServiceRegistered);
        }

        private void CreateButtons(HexTypeDefinition[] definitions)
        {
            List<GameObject> buttonList = new List<GameObject>();

            foreach(HexTypeDefinition definition in definitions)
            {
                GameObject button = Instantiate(m_hexButtonPrefab);
                button.transform.parent = transform;

                Image image = button.GetComponentInChildren<Image>();
                image.color = definition.TypeColor;

                HexButtonBehaviour hexButton = button.GetComponentInChildren<HexButtonBehaviour>();
                hexButton.SetDefinition(definition);

                buttonList.Add(button);
            }

            buttonList[buttonList.Count - 1].GetComponentInChildren<HexButtonBehaviour>().OnClicked();
        }
        #endregion

        #region Utility Methods
        private void HandleHexDefinitionServiceRegistered()
        {
            CreateButtons(m_hexDefinitionService.Reference.GetHexDefinitions());
        }
        #endregion
    }
}
