using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MinMaxValue<T>
{
    public T minValue;
    public T maxValue;
    public MinMaxValue()
    {

    }
    public MinMaxValue(T value)
    {
        minValue = value;
        maxValue = value;
    }
    public MinMaxValue(T min, T max)
    {
        minValue = min;
        maxValue = max;
    }
    public abstract T GetRandomValue();
}

// Float
[System.Serializable]
public class MinMaxValueFloat : MinMaxValue<float>
{
    public MinMaxValueFloat(float min, float max) : base(min, max)
    {
    }
    public override float GetRandomValue() => Random.Range(minValue, maxValue);
}

// Int
[System.Serializable]
public class MinMaxValueInt : MinMaxValue<int>
{
    public MinMaxValueInt(int min, int max) : base(min, max)
    {
    }
    public override int GetRandomValue() => Random.Range(minValue, maxValue);
}
// Int
[System.Serializable]
public class MinMaxValueBool : MinMaxValue<bool>
{
    public MinMaxValueBool(bool min, bool max) : base(min, max)
    {
    }
    public override bool GetRandomValue()
    {
        return Random.Range(0, 1) == 0 ? minValue : maxValue;
    }
}