using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class MinMaxSliderAttribute : PropertyAttribute
{
    public float Min { get; }
    public float Max { get; }

    public MinMaxSliderAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}
