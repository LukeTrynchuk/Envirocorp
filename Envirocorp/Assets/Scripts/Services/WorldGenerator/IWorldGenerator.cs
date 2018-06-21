using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// IWorld generator is a contract that
    /// all world generators must implement.
    /// </summary>
    public interface IWorldGenerator : IService
    {
        void GenerateWorld(int width, int height);
    }
}
