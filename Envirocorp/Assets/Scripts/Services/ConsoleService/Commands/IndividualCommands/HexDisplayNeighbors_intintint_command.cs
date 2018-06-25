using UnityEngine;
using FireBullet.Core.Services;
using FireBullet.Enviro.Board;
using System.Linq;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// Hex display neighbors intintint command
    /// will tell a hex at a specified positon to
    /// display lines connected to their neighbors.
    /// </summary>
    [CreateAssetMenu(fileName = "HexDisplayNeighbors_intintint_command", menuName = "Console Commands / HexDisplayNeighbors_intintint_command")]
    public class HexDisplayNeighbors_intintint_command : Command
    {
        [HideInInspector] public override string CommandString => "Hex Display Neighbors";
        [HideInInspector] public override string CommandDefinition => "Display Hex Neighbors of a hex given a coordinate (int,int,int)";
        [HideInInspector] public override bool HasParameters => true;

        public override string Execute(string parameter)
        {
            string[] values = parameter.Split(new char[] { ' ', '(', ')', ',' }).Where(x => !x.Contains(' ') &&
                                                                                      !x.Contains('(') &&
                                                                                       !x.Contains(')') &&
                                                                                      !x.Contains(',') &&
                                                                                      !x.Equals("")).ToArray();
            int[] numericValues = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                try
                {
                    numericValues[i] = int.Parse(values[i]);
                }
                catch (System.Exception e)
                {
                    return $"Error : Invalid parameter input {parameter}";
                }
            }

            if (numericValues.Length != 3) return $"Error : Invalid number of parameters {parameter}";

            ServiceReference<IBoardService> m_boardService = new ServiceReference<IBoardService>();
            if (!m_boardService.isRegistered()) return "Error : Board Service not registered";

            HexCell cell = m_boardService.Reference.GetCellAt(new HexCoordinate(numericValues[0], numericValues[2]));

            if (cell == null) return $"Error : No such hex exists at coordinates {parameter}";

            cell.DisplayNeighbors();

            return $"Display Neighbors at {parameter} successful";
        }
    }
}
