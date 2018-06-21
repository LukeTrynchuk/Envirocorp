using UnityEngine;

namespace FireBullet.Enviro.General.GameObjects
{
    /// <summary>
    /// Do not destoy on load ensures that the 
    /// gameobject that this component is attached to
    /// will not be destroyed on load.
    /// </summary>
    public class DoNotDestoyOnLoad : MonoBehaviour
    {
        #region Main Methods
        private void Start() => DontDestroyOnLoad(gameObject);
		#endregion
	}
}
