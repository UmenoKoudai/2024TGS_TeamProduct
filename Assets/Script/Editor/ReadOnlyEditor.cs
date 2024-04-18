using UnityEditor;
using UnityEngine;

/// <summary>PropertyDrawerを継承してインスペクターでの表示を変更</summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //操作不可にする
        EditorGUI.BeginDisabledGroup(true);
        //フィールドの表示
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndDisabledGroup();
    }
}