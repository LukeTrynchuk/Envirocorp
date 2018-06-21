using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FireBullet.Enviro.Scenes
{
    /// <summary>
    /// The Scene Advancer will advance to the next
    /// sequential scene in the build settings
    /// when asked to advance the scene.
    /// </summary>
    public class SceneAdvancer : MonoBehaviour
    {
        #region Main Methods
        public void AdvanceScene()
        {
            Scene currentScene = GetCurrentScene();
            AdvanceScene(currentScene);
        }
        #endregion

        #region Utility Methods
		private void AdvanceScene(Scene currentScene)
		{
            if (currentScene.buildIndex >= SceneManager.sceneCountInBuildSettings - 1) return;
            SceneManager.LoadScene(currentScene.buildIndex + 1);
		}
        #endregion

        #region Low Level Functions
        private Scene GetCurrentScene() => SceneManager.GetActiveScene();
        #endregion
    }
}
