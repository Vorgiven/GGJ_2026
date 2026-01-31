using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightAttribute : PropertyAttribute
{
    public readonly float r, g, b, a;

    public HighlightAttribute(float r, float g, float b, float a = 1f)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
    public Color Color => new Color(r, g, b, a); // helper to reconstruct
}
