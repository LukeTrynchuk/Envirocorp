using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// Hex display data intintint command
    /// will display general data in the console
    /// about a specified hex on the board. 
    /// </summary>
    [CreateAssetMenu(fileName = "HexDisplayData_intintint_Command", menuName = "Console Commands / HexDisplayData_intintint_Command")]
    public class HexDisplayData_intintint_Command : Command 
    {
        [HideInInspector] public override string CommandString => "Hex Display Data (,,)";
        [HideInInspector] public override string CommandDefinition => "Display Data about a hex given a coordinate";

        public override string Execute()
        {
            return "Data";
        }
    }
}
