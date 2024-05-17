using UnityEditor;
using UnityEngine;

/// <summary>PropertyDrawer���p�����ăC���X�y�N�^�[�ł̕\����ύX</summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //����s�ɂ���
        EditorGUI.BeginDisabledGroup(true);
        //�t�B�[���h�̕\��
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndDisabledGroup();
    }
}