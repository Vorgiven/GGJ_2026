using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HighlightAttribute))]
public class HighlightAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HighlightAttribute attribute = (HighlightAttribute)this.attribute;
        Color prevColor = GUI.color;
        GUI.color = new Color(attribute.r, attribute.g, attribute.b, attribute.a);
        EditorGUI.PropertyField(position, property, label);
        GUI.color = prevColor;
    }
}
