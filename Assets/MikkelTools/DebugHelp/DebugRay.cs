using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DebugRay
{
    public float Magnitude { get; set; }
    public Vector3 Origin { get; set; }
    public Vector3 Direction { get; set; }

    public Color Color { get; set; }

    public DebugRay(Vector3 origin, Vector3 direction, Color color, float magnitude = Mathf.Infinity)
    {
        Origin = origin;
        Direction = direction;
        Color = color;
        Magnitude = magnitude;
    }
}
