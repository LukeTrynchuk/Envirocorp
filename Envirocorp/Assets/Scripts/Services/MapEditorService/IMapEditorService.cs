using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// IMapEditorService is a contract that
    /// all map editors must implement. A map 
    /// editor is responsble for editing a map
    /// during runtime.
    /// </summary>
    public interface IMapEditorService : IService
    {
        void Activate(bool value);
    }
}
