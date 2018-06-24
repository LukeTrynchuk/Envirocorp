using UnityEngine;
using FireBullet.Core.Services;
using System.Linq;

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

            return $"Displaying Hex Data {parameter} \n" +
                $"{numericValues[0].ToString()} {numericValues[1].ToString()} {numericValues[2].ToString()}";
        }
    }
}
