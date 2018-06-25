using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// IFPSV isualizer service is a contract that
    /// all fps visualizers must implement.
    /// </summary>
    public interface IFPSVisualizerService : IService
    {
        void Visualize(bool value);
    }
}
