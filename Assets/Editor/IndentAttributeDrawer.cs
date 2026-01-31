using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(IndentAttribute))]
public class IndentAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        IndentAttribute indentAttribute = (IndentAttribute)attribute;

        int indent = Mathf.Abs(indentAttribute.indentation);
        EditorGUI.PropertyField(new Rect(position.x+ indent, position.y, position.width- indent, position.height), property, label);
    }

}
