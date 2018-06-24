using UnityEngine;
using FireBullet.Core.Services;
using TMPro;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// FPSV isualizer service is responsible for
    /// visualizing the FPS of the game.
    /// </summary>
    public class FPSVisualizerService : MonoBehaviour, IFPSVisualizerService
    {
        #region Private Variables
        [SerializeField]
        private TMP_Text m_text;

        private int m_frames = 0;
        private float m_timePassed = 0f;
        #endregion

        #region Main Methods
        void Start() => RegisterService();

        public void RegisterService()
        {
            ServiceLocator.Register<IFPSVisualizerService>(this);
        }

        public void Visualize(bool value) => m_text.gameObject.SetActive(value);

        void Update() 
        {
            m_timePassed += Time.unscaledDeltaTime;
            m_frames++;
            if(m_timePassed >= 1f)
            {
                m_timePassed = 0f;
                m_text.text = m_frames.ToString();
                m_frames = 0;
            }
        }
        #endregion
    }
}
