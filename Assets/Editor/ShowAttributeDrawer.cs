using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowAttribute))]
public class ShowAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);

        EditorGUI.LabelField(position, label.text);

        string displayText = "";

        switch (property.propertyType)
        {
            case SerializedPropertyType.Generic:
                displayText = "(Generic)";
                break;
            case SerializedPropertyType.Integer:
                displayText = property.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                displayText = property.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                displayText = property.floatValue.ToString("0.###");
                break;
            case SerializedPropertyType.String:
                displayText = property.stringValue;
                break;
            case SerializedPropertyType.Color:
                displayText = property.colorValue.ToString(); // Outputs as RGBA(r, g, b, a)
                break;
            case SerializedPropertyType.ObjectReference:
                displayText = property.objectReferenceValue != null ? property.objectReferenceValue.name : "None (Object)";
                break;
            case SerializedPropertyType.LayerMask:
                displayText = property.intValue.ToString(); // Not super helpful without conversion to layer name
                break;
            case SerializedPropertyType.Enum:
                displayText = property.enumDisplayNames[property.enumValueIndex];
                break;
            case SerializedPropertyType.Vector2:
                displayText = property.vector2Value.ToString();
                break;
            case SerializedPropertyType.Vector3:
                displayText = property.vector3Value.ToString();
                break;
            case SerializedPropertyType.Vector4:
                displayText = property.vector4Value.ToString();
                break;
            case SerializedPropertyType.Rect:
                displayText = property.rectValue.ToString();
                break;
            case SerializedPropertyType.ArraySize:
                displayText = property.intValue.ToString();
                break;
            case SerializedPropertyType.Character:
                displayText = ((char)property.intValue).ToString();
                break;
            case SerializedPropertyType.AnimationCurve:
                displayText = property.animationCurveValue.ToString(); // Just shows curve name
                break;
            case SerializedPropertyType.Bounds:
                displayText = property.boundsValue.ToString();
                break;
            case SerializedPropertyType.Gradient:
                displayText = "(Gradient)"; // No direct access; requires GradientField workaround
                break;
            case SerializedPropertyType.Quaternion:
                displayText = property.quaternionValue.eulerAngles.ToString() + " (Euler)";
                break;
            case SerializedPropertyType.ExposedReference:
                displayText = property.exposedReferenceValue != null ? property.exposedReferenceValue.name : "None (Exposed)";
                break;
            case SerializedPropertyType.FixedBufferSize:
                displayText = property.intValue.ToString();
                break;
            case SerializedPropertyType.Vector2Int:
                displayText = property.vector2IntValue.ToString();
                break;
            case SerializedPropertyType.Vector3Int:
                displayText = property.vector3IntValue.ToString();
                break;
            case SerializedPropertyType.RectInt:
                displayText = property.rectIntValue.ToString();
                break;
            case SerializedPropertyType.BoundsInt:
                displayText = property.boundsIntValue.ToString();
                break;
            case SerializedPropertyType.ManagedReference:
                displayText = "(Managed Reference)";
                break;
            case SerializedPropertyType.Hash128:
                displayText = property.hash128Value.ToString();
                break;
            default:
                displayText = "(Unknown)";
                break;
        }

        EditorGUI.LabelField(new Rect(position.x + EditorGUIUtility.labelWidth, position.y, EditorGUIUtility.labelWidth, position.height), displayText);
    }
}
