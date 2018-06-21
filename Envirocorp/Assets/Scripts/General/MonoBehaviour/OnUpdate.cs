using UnityEngine;
using UnityEngine.Events;

namespace FireBullet.Enviro.General
{
    /// <summary>
    /// The On Update component will invoke a 
    /// unity event every update for other
    /// components to subscribe to.
    /// </summary>
    public class OnUpdate : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private UnityEvent m_onUpdate;
        #endregion

        #region Main Methods
        private void Update() => m_onUpdate?.Invoke();
		#endregion
	}
}
