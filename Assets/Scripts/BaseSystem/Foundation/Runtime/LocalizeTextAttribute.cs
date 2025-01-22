using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class LocalizeTextAttribute : PropertyAttribute
{
    public string Master { get; protected set; }
    public LocalizeTextAttribute(string masterName = "Global")
    {
        Master = masterName;
    }
}
