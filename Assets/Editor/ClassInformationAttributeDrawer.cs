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

        GUIStyle guiStyleTitle = new GUIStyle();
        guiStyleTitle.alignment = TextAnchor.MiddleCenter;
        guiStyleTitle.fontSize = 24;
        guiStyleTitle.fontStyle = FontStyle.Bold;
        guiStyleTitle.normal.textColor = Color.red; 

        GUIStyle guiStyleDescription = new GUIStyle();
        guiStyleDescription.alignment = TextAnchor.MiddleCenter;
        guiStyleDescription.fontSize = 12;
        guiStyleDescription.fontStyle = FontStyle.Italic;
        guiStyleDescription.normal.textColor = Color.white;


        if (attribute != null)
        {
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
