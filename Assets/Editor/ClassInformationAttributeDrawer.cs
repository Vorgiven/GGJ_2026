using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class ClassInformationDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        var targetType = target.GetType();
        var attribute = (ClassInformationAttribute)Attribute.GetCustomAttribute(targetType, typeof(ClassInformationAttribute));


        if (attribute != null)
        {
            GUIStyle guiStyleTitle = new GUIStyle();
            guiStyleTitle.alignment = TextAnchor.MiddleCenter;
            guiStyleTitle.fontSize = attribute.titleFontSize;
            guiStyleTitle.fontStyle = FontStyle.Bold;
            guiStyleTitle.normal.textColor = Color.red;

            GUIStyle guiStyleDescription = new GUIStyle();
            guiStyleDescription.alignment = TextAnchor.MiddleCenter;
            guiStyleDescription.fontSize = attribute.descriptionFontSize;
            guiStyleDescription.fontStyle = FontStyle.Italic;
            guiStyleDescription.normal.textColor = Color.white;

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(attribute.title, guiStyleTitle);
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField(attribute.description, guiStyleDescription);
            EditorGUILayout.Space(10);
            //EditorGUILayout.HelpBox(attribute.description, MessageType.Info);
        }

        DrawDefaultInspector();
    }
}
