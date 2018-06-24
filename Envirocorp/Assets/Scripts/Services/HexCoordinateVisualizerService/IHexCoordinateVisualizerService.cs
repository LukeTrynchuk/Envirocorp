using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// IHexCoordinateVisualizerService is a service that controls
    /// the visualization of hex coordinates in the game view.
    /// 
    /// All HexCoordinateVisualizers must implement this interface.
    /// </summary>
    public interface IHexCoordinateVisualizerService : IService
    {
        bool visible { get; }
        void Visualize(bool value);
        void CreateHexCoordinate(Vector3 position, int x, int y, HexCell cell);
        void ClearVisualization();
    }
}
