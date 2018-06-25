using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// Hex stop displaying neighbors will tell
    /// all the hexes on the board to stop
    /// displaying their neighbors.
    /// </summary>
    [CreateAssetMenu(fileName = "HexStopDisplayingNeighbors", menuName = "Console Commands / HexStopDisplayingNeighbors")]
    public class HexStopDisplayingNeighbors : Command
    {
        [HideInInspector] public override string CommandString => "Hex Stop Displaying Neighbors";
        [HideInInspector] public override string CommandDefinition => "Stop Displaying Hex Neighbors";
        [HideInInspector] public override bool HasParameters => false;

        public override string Execute()
        {
            ServiceReference<IBoardService> m_boardService = new ServiceReference<IBoardService>();
            if (!m_boardService.isRegistered()) return "Error : Board service is not registered";

            HexCell[] cells = m_boardService.Reference.GetBoard();
            foreach (HexCell cell in cells)
                cell.StopDisplayingNeighbors();

            return "Stopped displaying neighbors successfully";
        }
    }
}
