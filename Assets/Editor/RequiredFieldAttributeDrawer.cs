using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
public class RequiredFieldAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
       
        RequiredFieldAttribute RequiredFieldAttribute = (RequiredFieldAttribute)attribute;
        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        if (property.objectReferenceValue != null)
        {
            GUIContent requiredLabel = new GUIContent($"{label.text}*");
            EditorGUI.PropertyField(position, property, requiredLabel);
        }
        else
        {
            GUI.contentColor = new Color(1f, 0, 0);
            GUIContent requiredLabel = new GUIContent($"{label.text} [Required]");
            EditorGUI.PropertyField(position, property, requiredLabel);
            GUI.contentColor = Color.white;
        }
    }
}
