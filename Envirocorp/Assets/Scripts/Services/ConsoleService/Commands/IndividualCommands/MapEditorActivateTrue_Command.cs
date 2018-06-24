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

        public override string Execute() 
        {
            return "Map Editor opened";
        }
    }
}
