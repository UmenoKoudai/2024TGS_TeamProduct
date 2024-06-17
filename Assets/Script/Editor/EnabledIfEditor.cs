using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnabledIfAttribute))]
public class EnabledIfAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = attribute as EnabledIfAttribute;
        var isEnabled = GetIsEnabled(attr, property);

        // プロパティの描画
        if (isEnabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        //非表示の時　かつ　子要素があれば
        if (!isEnabled && property.hasChildren)
        {
            //折り畳む
            property.isExpanded = false;
        }

        GUI.enabled = isEnabled;
    }

    /// <summary> 表示されているかどうか </summary>
    private bool GetIsVisible(EnabledIfAttribute attribute, SerializedProperty property)
    {
        if (GetIsEnabled(attribute, property))
        {
            return true;
        }
        return false;
    }

    /// <summary>条件が一致していたら</summary>
    private bool GetIsEnabled(EnabledIfAttribute attribute, SerializedProperty property)
    {
        return attribute.enableIfValueIs == GetSwitcherPropertyValue(attribute, property);
    }

    /// <summary>指定したProperty(bool型)の値を持ってくる</summary>
    private bool GetSwitcherPropertyValue(EnabledIfAttribute attribute, SerializedProperty property)
    {
        var propertyNameIndex = property.propertyPath.LastIndexOf(property.name, StringComparison.Ordinal);
        var switcherPropertyName = property.propertyPath.Substring(0, propertyNameIndex) + attribute.switcherFieldName;
        var switcherProperty = property.serializedObject.FindProperty(switcherPropertyName);
        if(switcherProperty.propertyType == SerializedPropertyType.Boolean)
        {
            return switcherProperty.boolValue;
        }
        else
        {
            throw new System.Exception("unsupported type.");
        }
    }
}
