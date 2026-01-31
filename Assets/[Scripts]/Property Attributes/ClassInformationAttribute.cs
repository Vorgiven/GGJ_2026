using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class ClassInformationAttribute : Attribute
{
    public readonly string title;
    public readonly string description;

    public ClassInformationAttribute(string title, string description)
    {
        this.title = title;
        this.description = description;
    }
}
