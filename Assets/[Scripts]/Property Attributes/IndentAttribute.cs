using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class IndentAttribute : PropertyAttribute
{
    public readonly int indentation;
    public IndentAttribute(int indentation=10)
    {
        this.indentation = indentation;
    }
}
