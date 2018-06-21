using UnityEngine;
using UnityEngine.Events;

namespace FireBullet.Enviro.General
{
    /// <summary>
    /// The On Start component will invoke 
    /// a unity event on start for other components
    /// to subscribe to.
    /// </summary>
    public class OnStart : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private UnityEvent m_onStart;
        #endregion

        #region Main Methods
        private void Start() => m_onStart?.Invoke();
		#endregion
	}
}
