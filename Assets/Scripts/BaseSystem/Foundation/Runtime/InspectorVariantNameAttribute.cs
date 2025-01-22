using System;
using UnityEngine;

/// <summary>
/// エディタ拡張で変数名を指定できるようにする
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class InspectorVariantNameAttribute : PropertyAttribute
{
    string _variantName = "";
    public string VariantName => _variantName;

    public InspectorVariantNameAttribute(string name)
    {
        _variantName = name;
    }
}
