using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// IWorld generator is a contract that
    /// all world generators must implement.
    /// </summary>
    public interface IWorldGenerator : IService
    {
        event System.Action<HexCell[], int, int> OnWorldGenerated;

        void GenerateWorld(int width, int height);
        void RetriangulateWorld();
        void VisualizeGridCoordinates(bool value);
    }
}
