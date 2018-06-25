using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// Grid Coordinates activate false command is
    /// a command that can be used to turn off the
    /// grid coordinate visualization system.
    /// </summary>
    [CreateAssetMenu(fileName = "GridCoordinatesActivateFalse_Command", menuName = "Console Commands / GridCoordinatesActivateFalse_Command")]
    public class GridCoordinateActivateFalse_Command : Command
    {
        [HideInInspector] public override string CommandString => "Grid Coordinates Activate false";
        [HideInInspector] public override string CommandDefinition => "Deactivate the visualization of grid coordinates";
        [HideInInspector] public override bool HasParameters => false;

        public override string Execute()
        {
            ServiceReference<IHexCoordinateVisualizerService> m_hexVisualizer 
                    = new ServiceReference<IHexCoordinateVisualizerService>();

            if (!m_hexVisualizer.isRegistered()) return "Visualize Grid Coordinates failed : Visualizer not registered";

            m_hexVisualizer.Reference.Visualize(false);
            return "Deactivate Grid Coordinates successful";
        }
    }
}
