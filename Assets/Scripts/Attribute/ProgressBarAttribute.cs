using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ProgressBarAttribute : PropertyAttribute
{
    public float Min { get; }
    public float Max { get; }
    public string Label { get; }

    public ProgressBarAttribute(float min, float max, string label = "")
    {
        Min = min;
        Max = max;
        Label = label;
    }
}