using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UtilitiesEditor;
using static UnityEditor.EditorGUI;

[CustomPropertyDrawer(typeof(Position2D))]
public class Position2DDrawer : PropertyDrawer
{
    static readonly GUIContent CopyButtonContent = new("Copy Current", $"Set this {nameof(Position2D)} values to the current Transform values.");

    struct Properties
    {
        public SerializedObject parent;
        public SerializedProperty position;
        public SerializedProperty rotation;

        public Properties(SerializedProperty parentProp)
        {
            parent = parentProp.serializedObject;
            position = parentProp.FindPropertyRelative(nameof(position));
            rotation = parentProp.FindPropertyRelative(nameof(rotation));
        }
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        Properties properties = new(property);

        label = BeginProperty(rect, label, property);

        LabelAndButtonGUI(rect, properties, label);

        PropertiesGUI(rect, properties);

        EndProperty();
    }

    void LabelAndButtonGUI(Rect rect, Properties properties, GUIContent label)
    {
        rect = PrefixLabel(new Rect(rect) { height = EditorGUIUtility.singleLineHeight }, label);

        if (properties.parent.isEditingMultipleObjects)
            return;

        Transform transform;
        try { transform = ((dynamic)properties.parent.targetObject).transform; }
        catch { return; }

        if (GUI.Button(rect, CopyButtonContent))
        {
            SetValuesToCurrentTransform(properties, transform);
        }
    }

    void SetValuesToCurrentTransform(Properties properties, Transform transform)
    {
        properties.position.vector2Value = transform.position;
        properties.rotation.floatValue = Mathf.DeltaAngle(0, transform.eulerAngles.z);
    }

    void PropertiesGUI(Rect rect, Properties properties)
    {
        using (new GUIBlock.Indent())
        {
            rect.height = EditorGUIUtility.singleLineHeight;

            rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;

            PropertyField(rect, properties.position);

            rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;

            PropertyField(rect, properties.rotation);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 3f + EditorGUIUtility.standardVerticalSpacing * 2f;
    }
}
