using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{

    /// <summary>
    /// Grid Coordinates activate true command is 
    /// a command that can be used to turn on the
    /// grid coordinate visualization system.
    /// </summary>
    [CreateAssetMenu(fileName = "GridCoordinatesActivateTrue_Command", menuName = "Console Commands / GridCoordinatesActivateTrue_Command")]
    public class GridCoordinatesActivateTrue_Command : Command
    {
        [HideInInspector] public override string CommandString => "Grid Coordinates Activate true";
        [HideInInspector] public override string CommandDefinition => "Activate the visualization of grid coordinates";

		public override string Execute()
		{
            ServiceReference<IWorldGenerator> m_worldGenerator = new ServiceReference<IWorldGenerator>();
            if (!m_worldGenerator.isRegistered()) return "Visualize Grid Coordinates failed : World Generator not registered";

            m_worldGenerator.Reference.VisualizeGridCoordinates(true);
            return "Visualize Grid Coordinates successful";
		}
	}
}
