using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMax))]
public class MinMaxDrawer : PropertyDrawer
{
    const float minLabelWidth = 22f;
    const float maxLabelWidth = 26f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty minProp = property.FindPropertyRelative("_min");
        SerializedProperty maxProp = property.FindPropertyRelative("_max");
        bool doNotEnforceMinInferiorToMax = property.FindPropertyRelative("doNotEnforceMinInferiorToMax").boolValue;

        int oldIndentLevel = EditorGUI.indentLevel;

        string tooltipText = null;
        TooltipAttribute tooltip = GetTooltip(fieldInfo, true);
        if (tooltip != null)
            tooltipText = tooltip.tooltip;

        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(property.displayName, tooltipText));

        contentPosition.height = 18f;

        if (!EditorGUIUtility.wideMode)
        {
            position.height = 18f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 20f;
        }

        float half = contentPosition.width / 2;
        //GUI.skin.label.padding = new RectOffset(3, 3, 6, 6);

        //show the min and max
        EditorGUIUtility.labelWidth = minLabelWidth;
        contentPosition.width = half - 2;
        EditorGUI.indentLevel = 0;

        // Begin/end property & change check make each field
        // behave correctly when multi-object editing.
        EditorGUI.BeginProperty(contentPosition, label, minProp);
        {
            EditorGUI.BeginChangeCheck();
            float newVal = EditorGUI.DelayedFloatField(contentPosition, new GUIContent("Min"), minProp.floatValue);
            if (EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = newVal;
                if (!doNotEnforceMinInferiorToMax && minProp.floatValue > maxProp.floatValue)
                    maxProp.floatValue = minProp.floatValue;
            }
        }
        EditorGUI.EndProperty();

        EditorGUIUtility.labelWidth = maxLabelWidth;
        contentPosition.x += half;
        contentPosition.width = half;

        // Begin/end property & change check make each field
        // behave correctly when multi-object editing.
        EditorGUI.BeginProperty(contentPosition, label, maxProp);
        {
            EditorGUI.BeginChangeCheck();
            float newVal = EditorGUI.DelayedFloatField(contentPosition, new GUIContent("Max"), maxProp.floatValue);
            if (EditorGUI.EndChangeCheck())
            {
                maxProp.floatValue = newVal;
                if (!doNotEnforceMinInferiorToMax && maxProp.floatValue < minProp.floatValue)
                    minProp.floatValue = maxProp.floatValue;
            }
        }
        EditorGUI.EndProperty();

        EditorGUI.indentLevel = oldIndentLevel;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return !EditorGUIUtility.wideMode ? 38f : 20f;
    }

    TooltipAttribute GetTooltip(FieldInfo field, bool inherit)
    {
        TooltipAttribute[] attributes = field.GetCustomAttributes(typeof(TooltipAttribute), inherit) as TooltipAttribute[];

        return attributes.Length > 0 ? attributes[0] : null;
    }
}