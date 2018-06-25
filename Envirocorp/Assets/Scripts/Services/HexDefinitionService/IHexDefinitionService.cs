using UnityEngine;
using FireBullet.Enviro.Board;
using FireBullet.Core.Services;

namespace FireBullet.Core.Services
{
    /// <summary>
    /// The IHexDefinitionService is a contract that
    /// all hex defintion services must implement.
    /// A hex definition service is responsible for 
    /// providing all hex defintions to other services
    /// and systems.
    /// </summary>
    public interface IHexDefinitionService : IService
    {
        HexTypeDefinition[] GetHexDefinitions();
    }
}
