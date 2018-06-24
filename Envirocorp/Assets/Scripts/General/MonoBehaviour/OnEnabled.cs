using UnityEngine;
using UnityEngine.Events;

namespace FireBullet.Enviro.General
{
    /// <summary>
    /// The OnEnabled component can be 
    /// attached to any game object and
    /// will invoke a unity event inside
    /// OnEnable.
    /// </summary>
    public class OnEnabled : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private UnityEvent m_onEnabled;
        #endregion

        #region Main Methods
        private void OnEnable() => m_onEnabled?.Invoke();
		#endregion
	}
}
