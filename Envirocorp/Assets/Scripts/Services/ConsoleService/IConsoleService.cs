using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The IConsoleService interface defines the
    /// contract that all console services must 
    /// implement.
    /// </summary>
    public interface IConsoleService : IService
    {
        bool Active { get; }
    }
}
