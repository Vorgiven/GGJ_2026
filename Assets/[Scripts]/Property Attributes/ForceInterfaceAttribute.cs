using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceInterfaceAttribute : PropertyAttribute
{
    // type - store something such as class, interface, enums etc..
    public readonly Type interfaceType;

    public ForceInterfaceAttribute(Type interfaceType)
    {
        this.interfaceType = interfaceType;
    }
}
