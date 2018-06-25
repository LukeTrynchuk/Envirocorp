using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The Map Editor Activate true command will
    /// activate the map editor to true.
    /// </summary>
    [CreateAssetMenu(fileName = "MapEditorActivateTrue_Command", menuName = "Console Commands / MapEditorActivateTrue_Command")]
    public class MapEditorActivateTrue_Command : Command
    {
        [HideInInspector] public override string CommandString => "MapEditor activate true";
        [HideInInspector] public override string CommandDefinition => "Activate the In Game Map Editor";
        [HideInInspector] public override bool HasParameters => false;

        public override string Execute() 
        {
            ServiceReference<IMapEditorService> m_mapEditorService = new ServiceReference<IMapEditorService>();
            if(!m_mapEditorService.isRegistered()) return "Map Editor Close failed : Map Editor not registered";

            m_mapEditorService.Reference.Activate(true);
            return "Map Editor opened successfully";
        }
    }
}
