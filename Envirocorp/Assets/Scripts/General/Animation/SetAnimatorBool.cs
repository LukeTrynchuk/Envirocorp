using UnityEngine;

namespace FireBullet.Enviro.General
{
    /// <summary>
    /// Set Animator bool component will set a
    /// bool parameter of an animator when 
    /// given a value.
    /// </summary>
    public class SetAnimatorBool : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private Animator m_animator;

        [SerializeField]
        private string m_parameterName;
        #endregion

        #region Main Methods
        public void SetValue(bool value) => m_animator.SetBool(m_parameterName, value);
        #endregion
    }
}
