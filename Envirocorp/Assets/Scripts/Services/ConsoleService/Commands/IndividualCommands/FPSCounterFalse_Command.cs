﻿using UnityEngine;
using FireBullet.Core.Services;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// FPSC ounter false command is a command
    /// that turns off the FPS counter.
    /// </summary>
    [CreateAssetMenu(fileName = "FPSCounterFalse_Command", menuName = "Console Commands / FPSCounterFalse_Command")]
    public class FPSCounterFalse_Command : Command
    {
        [HideInInspector] public override string CommandString => "FPS Counter false";
        [HideInInspector] public override string CommandDefinition => "Deactivate the FPS Counter";

        public override string Execute()
        {
            ServiceReference<IFPSVisualizerService> m_fpsCounter = new ServiceReference<IFPSVisualizerService>();

            if (!m_fpsCounter.isRegistered()) return "Error : FPS Counter not registered";

            m_fpsCounter.Reference.Visualize(false);
            return "FPS Counter off successful";
        }
    }
}
