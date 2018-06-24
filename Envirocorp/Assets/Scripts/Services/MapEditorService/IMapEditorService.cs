using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;

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
        event System.Action<HexTypeDefinition> OnBrushChanged;

        void Activate(bool value);
        void SetCurrentHexBrush(HexTypeDefinition definition);
    }
}
