using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public override string Execute()
        {
            return "Map Editor closed";
        }
    }
}
