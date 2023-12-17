using UnityEngine;
using UnityEditor;
using System.Linq;


[CustomPropertyDrawer(typeof(System.Type))]
public class TypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Check if the SerializedProperty type is actually a System.Type
        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue != null && property.objectReferenceValue.GetType() == typeof(System.Type))
        {
            // Display a popup that lists all available system types in the current assembly
            var typeNames = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract)
                .Select(type => type.FullName)
                .ToArray();

            var selectedTypeIndex = 0;
            var selectedTypeName = property.objectReferenceValue.ToString();

            for (var i = 0; i < typeNames.Length; i++)
            {
                if (typeNames[i] == selectedTypeName)
                {
                    selectedTypeIndex = i;
                    break;
                }
            }

            selectedTypeIndex = EditorGUI.Popup(position, label.text, selectedTypeIndex, typeNames);

            // Update the SerializedProperty with the selected System.Type
            var selectedType = System.Type.GetType(typeNames[selectedTypeIndex]);
            var tempObject = ScriptableObject.CreateInstance(selectedType);
            var serializedObject = new SerializedObject(tempObject);
            var typeProperty = serializedObject.FindProperty("m_Script");
            property.objectReferenceValue = typeProperty.objectReferenceValue;

            UnityEngine.Object.DestroyImmediate(tempObject);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }

        EditorGUI.EndProperty();
    }
}
