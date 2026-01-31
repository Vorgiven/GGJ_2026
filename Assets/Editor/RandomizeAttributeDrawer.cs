using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RandomizeAttribute))]
public class RandomizeAttributeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 32f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUI.PropertyField(position, property);

        if (property.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.BeginProperty(position, label, property); // need this for undoing stuff
            float gap = 16f;
            Rect labelPosition = new Rect(position.x, position.y, position.width, gap);
            Rect buttonPosition = new Rect(position.x, position.y + labelPosition.height, position.width, gap);
            EditorGUI.LabelField(labelPosition, label, new GUIContent(property.floatValue.ToString()));

            if (GUI.Button(buttonPosition, "Randomize"))
            {
                RandomizeAttribute randomizeAttribute = (RandomizeAttribute)attribute;
                property.floatValue = Random.Range(randomizeAttribute.minValue, randomizeAttribute.maxValue);
            }
            EditorGUI.EndProperty(); // need this for undoing stuff
        }
        else
        {
            EditorGUI.LabelField(position, "Property Type must be a float");
        }
    }
}
