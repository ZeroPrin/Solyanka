using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class SectionHeaderAttribute : PropertyAttribute
{
    public readonly string Title;

    public SectionHeaderAttribute(string title)
    {
        Title = title;
    }
}
