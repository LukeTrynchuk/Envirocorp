using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Services;

namespace FireBullet.Enviro.General
{
    /// <summary>
    /// The Request for generated will request that
    /// a world be generated.
    /// </summary>
    public class RequestWorldGenerated : MonoBehaviour
    {
        #region Private Variables
        [SerializeField]
        private int m_width;

        [SerializeField]
        private int m_height;

        private ServiceReference<IWorldGenerator> m_worldGenerator = new ServiceReference<IWorldGenerator>();
        #endregion

        #region Main Methods
        public void GenerateWorld()
        {
            m_worldGenerator.Reference?.GenerateWorld(m_width, m_height);
        }
        #endregion
    }
}
