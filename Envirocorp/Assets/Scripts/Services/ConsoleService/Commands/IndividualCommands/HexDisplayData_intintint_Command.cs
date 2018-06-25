using UnityEngine;
using FireBullet.Core.Services;
using System.Linq;
using FireBullet.Enviro.Board;

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
        [HideInInspector] public override string CommandString => "Hex Display Data";
        [HideInInspector] public override string CommandDefinition => "Display Data about a hex given a coordinate (int,int,int)";
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
                catch(System.Exception e)
                {
                    return $"Error : Invalid parameter input {parameter}";
                }
            }

            if (numericValues.Length != 3) return $"Error : Invalid number of parameters {parameter}";

            ServiceReference<IBoardService> m_boardService = new ServiceReference<IBoardService>();
            if (!m_boardService.isRegistered()) return "Error : Board Service not registered";

            HexCell cell = m_boardService.Reference.GetCellAt(new HexCoordinate(numericValues[0], numericValues[2]));

            if (cell == null) return $"Error : No such hex exists at coordinates {parameter}";

            return "Hex Data Result : \n" +
                $"Color : {cell.m_Color.ToString()} \n" +
                $"Coordinates : {cell.m_Coordinate.ToString()}";

        }
    }
}
