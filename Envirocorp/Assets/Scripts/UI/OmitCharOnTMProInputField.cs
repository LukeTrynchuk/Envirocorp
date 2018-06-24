using UnityEngine;
using TMPro;

namespace FireBullet.Enviro.UI
{
    /// <summary>
    /// OmitCharOnTMProText will omit a specified
    /// char from a text mesh pro components text.
    /// </summary>
    public class OmitCharOnTMProInputField : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private TMP_InputField m_inputField;

        [SerializeField]
        private char m_charToOmit;
		#endregion

		#region Main Methods
		private void Start()
		{
            m_inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return ProcessText(addedChar); };
		}

		public char ProcessText(char charToValidate)
        {
            if (charToValidate.Equals(m_charToOmit)) return ' ';
            return charToValidate;
        }
        #endregion
    }
}
