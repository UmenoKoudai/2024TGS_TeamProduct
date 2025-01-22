using UnityEditor;
using UnityEngine;

/// <summary>
/// 変数名を日本語にするエディタ拡張
/// </summary>
[CustomPropertyDrawer(typeof(InspectorVariantNameAttribute))]
public class InspectorVariantNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = attribute as InspectorVariantNameAttribute;
        if(attr != null)
        {
            label.text = attr.VariantName;
        }
        EditorGUI.PropertyField(position, property, label, true);
    }
}
