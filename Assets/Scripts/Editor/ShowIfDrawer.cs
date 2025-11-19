#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    private SerializedProperty FindRelativeProperty(SerializedProperty property, string propertyName)
    {
        // Get parent path
        string path = property.propertyPath;
        int lastDot = path.LastIndexOf('.');
        string parentPath = lastDot >= 0 ? path.Substring(0, lastDot) : "";

        // Combine parent + target variable
        SerializedProperty parent = property.serializedObject.FindProperty(parentPath);
        if (parent == null) return null;

        return parent.FindPropertyRelative(propertyName);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var showIf = (ShowIfAttribute)attribute;
        var boolProp = FindRelativeProperty(property, showIf.boolName);

        if (boolProp == null)
            return EditorGUI.GetPropertyHeight(property);

        bool shouldShow = boolProp.boolValue == showIf.showWhenTrue;
        return shouldShow ? EditorGUI.GetPropertyHeight(property) : 0f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var showIf = (ShowIfAttribute)attribute;
        var boolProp = FindRelativeProperty(property, showIf.boolName);

        if (boolProp != null)
        {
            bool shouldShow = boolProp.boolValue == showIf.showWhenTrue;
            if (!shouldShow)
                return;
        }

        EditorGUI.PropertyField(position, property, label, true);
    }
}
#endif
