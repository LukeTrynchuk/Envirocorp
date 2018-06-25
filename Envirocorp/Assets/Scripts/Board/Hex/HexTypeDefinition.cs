using UnityEngine;

namespace FireBullet.Enviro.Board
{
    /// <summary>
    /// Hex type definition will define a certain
    /// type of hex that can be spawned onto the board.
    /// </summary>
    [CreateAssetMenu(fileName = "NewHexDefinition", menuName = "Board/Hex/Definition")]
    public class HexTypeDefinition : ScriptableObject
    {
        public string TypeName;
        public Color TypeColor;
    }
}
