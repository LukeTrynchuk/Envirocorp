using UnityEngine;
using UnityEditor;
using FireBullet.Enviro.Board;

[CustomPropertyDrawer(typeof(HexCoordinate))]
public class HexCoordinateDrawer : PropertyDrawer 
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
        HexCoordinate coordinate = new HexCoordinate(property.FindPropertyRelative("m_x").intValue,
                                                     property.FindPropertyRelative("m_z").intValue);

        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, coordinate.ToString());
	}
}
