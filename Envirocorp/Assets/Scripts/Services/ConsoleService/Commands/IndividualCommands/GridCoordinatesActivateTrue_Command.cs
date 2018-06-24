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
            ServiceReference<IHexCoordinateVisualizerService> m_hexVisualizer 
                    = new ServiceReference<IHexCoordinateVisualizerService>();

            if (!m_hexVisualizer.isRegistered()) return "Visualize Grid Coordinates failed : Visualizer not registered";

            m_hexVisualizer.Reference.Visualize(true);
            return "Visualize Grid Coordinates successful";
		}
	}
}
