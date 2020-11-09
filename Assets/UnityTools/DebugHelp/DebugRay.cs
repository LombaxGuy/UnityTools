using System;
using UnityEngine;

namespace UnityTools.Debugging
{
    public struct DebugRay
    {
        public float Magnitude { get; private set; }
        public Vector3 Origin { get; private set; }
        public Vector3 Direction { get; private set; }
        public Color Color { get; private set; }

        public DebugRay Empty { get => new DebugRay(); }

        public DebugRay(Vector3 origin, Vector3 direction, Color color, float magnitude = Mathf.Infinity)
        {
            Origin = origin;
            Direction = direction;
            Color = color;
            Magnitude = magnitude;
        }

        public void Draw()
        {
            Debug.DrawRay(Origin, Direction * Magnitude, Color);
        }

        public bool Equals(DebugRay compareTo)
        {
            if (compareTo.Magnitude == Magnitude &&
                compareTo.Origin == Origin &&
                compareTo.Direction == Direction &&
                compareTo.Color == Color)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}