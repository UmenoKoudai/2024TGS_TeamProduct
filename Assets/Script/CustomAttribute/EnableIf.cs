using UnityEngine;

/// <summary>�X�N���v�g�ϐ��̃J�X�^���������쐬���邽�߂̃N���X</summary>
[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, AllowMultiple = false)]
public class EnabledIfAttribute : PropertyAttribute
{
    public string switcherFieldName;
    public bool enableIfValueIs;

    public EnabledIfAttribute(string switcherFieldName, bool enableIfValueIs)
    {
        this.switcherFieldName = switcherFieldName;
        this.enableIfValueIs = enableIfValueIs;
    }
}
