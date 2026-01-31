using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class AutoFindComponentAttribute : PropertyAttribute
{
    public readonly Type childComponent;
    public readonly bool findInChildren;

    public AutoFindComponentAttribute(Type childComponent, bool findInChildren = false)
    {
        this.childComponent = childComponent;
        this.findInChildren = findInChildren;
    }
}
