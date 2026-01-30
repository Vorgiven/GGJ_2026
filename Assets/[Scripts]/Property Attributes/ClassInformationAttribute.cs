using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class ClassInformationAttribute : Attribute
{
    public readonly string title;
    public readonly string description;
    public readonly int titleFontSize;
    public readonly int descriptionFontSize;

    public ClassInformationAttribute(string title, string description, int titleFontSize=24, int descriptionFontSize=12)
    {
        this.title = title;
        this.description = description;
        this.titleFontSize = titleFontSize;
        this.descriptionFontSize = descriptionFontSize;
    }
}
