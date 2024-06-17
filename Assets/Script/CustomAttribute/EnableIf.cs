using UnityEngine;

/// <summary>スクリプト変数のカスタム属性を作成するためのクラス</summary>
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
