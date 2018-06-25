using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// FPSC ounter false command is a command
    /// that turns on the FPS counter.
    /// </summary>
    [CreateAssetMenu(fileName = "FPSCounterTrue_Command", menuName = "Console Commands / FPSCounterTrue_Command")]
    public class FPSCounterTrue_Command : Command
    {
        [HideInInspector] public override string CommandString => "FPS Counter true";
        [HideInInspector] public override string CommandDefinition => "Activate the FPS Counter";
        [HideInInspector] public override bool HasParameters => false;

        public override string Execute()
        {
            ServiceReference<IFPSVisualizerService> m_fpsCounter = new ServiceReference<IFPSVisualizerService>();

            if (!m_fpsCounter.isRegistered()) return "Error : FPS Counter not registered";

            m_fpsCounter.Reference.Visualize(true);
            return "FPS Counter on successful";
        }
    }
}
