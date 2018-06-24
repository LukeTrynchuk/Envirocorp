using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The Map Editor Activate false command
    /// will activate the map editor to false.
    /// </summary>
    [CreateAssetMenu(fileName = "MapEditorActivateFalse_Command", menuName = "Console Commands / MapEditorActivateFalse_Command")]
    public class MapEditorActivateFalse_Command : Command 
    {
        [HideInInspector] public override string CommandString => "MapEditor activate false";
        [HideInInspector] public override string CommandDefinition => "Deactivate the In Game Map Editor";
        [HideInInspector] public override bool HasParameters => false;

        public override string Execute()
        {
            ServiceReference<IMapEditorService> m_mapEditorService = new ServiceReference<IMapEditorService>();
            if (!m_mapEditorService.isRegistered()) return "Map Editor Close failed : Map Editor not registered";

            m_mapEditorService.Reference.Activate(false);
            return "Map Editor closed successfully";
        }
    }
}
