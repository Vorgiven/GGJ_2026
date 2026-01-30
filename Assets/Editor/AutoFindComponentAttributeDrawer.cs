using System.Linq;
using UnityEditor;
using UnityEngine;
//using Component = UnityEngine.Component;

[CustomPropertyDrawer(typeof(AutoFindComponentAttribute))]
public class AutoFindComponentAttributeDrawer : PropertyDrawer
{
    SerializedProperty array;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label);

        if (property.propertyType != SerializedPropertyType.ObjectReference)
            return;

        AutoFindComponentAttribute attribute = (AutoFindComponentAttribute)this.attribute;

        // Get the object that holds the field
        var targetObject = property.serializedObject.targetObject as Component;
        if (targetObject == null)
            return;

        // Search in children
        Component childComponent = null;
        Component[] childComponentArray = null;
        if (attribute.findInChildren)
            childComponent = targetObject.GetComponentInChildren(attribute.childComponent);
        else
            childComponent = targetObject.GetComponent(attribute.childComponent);

        childComponentArray = targetObject.GetComponentsInChildren(attribute.childComponent);

        //// For arrays
        string path = property.propertyPath;
        if (path.Contains('.') && array == null)
            array = property.serializedObject.FindProperty(path.Substring(0, path.LastIndexOf('.')));

        if (array != null)
        {
            if (array.arraySize != childComponentArray.Length)
                array.arraySize = childComponentArray.Length;

            for (int i = 0; i < array.arraySize; i++)
                array.GetArrayElementAtIndex(i).objectReferenceValue = childComponentArray[i];

        }
        else if (childComponent != null)
        {
            property.objectReferenceValue = childComponent;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
