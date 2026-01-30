using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class BaseValue<T>
{
    [SerializeField] protected T baseValue;
    [SerializeField] protected T currentValue;

    // empty constructor
    public BaseValue() { }
    // Constructor to set base value
    public BaseValue(T baseValue) => this.baseValue = baseValue;

    public void Initialize() => currentValue = baseValue;

    // Get current value
    public T GetCurrentValue() => currentValue;
    public T GetBaseValue() => baseValue;
    // Update current value
    public void SetCurrentValue(T value) => currentValue = value;
    // Set base value
    public void SetBaseValue(T value) => baseValue = value;
    // Set base value
    public void ResetValue(T value) => baseValue = value;
    // Reset value
    public void ResetValue() => currentValue = baseValue;
}


// Int base value

[System.Serializable]
public class BaseValueInt : BaseValue<int>
{
    // empty constructor
    public BaseValueInt() : base() { }
    // Constructor to set base value
    public BaseValueInt(int baseValue) : base(baseValue) => this.baseValue = baseValue;


    // Add current value for int
    public void AddCurrentValue(int value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, 0, baseValue);
    }
    // returns 0 - 1
    public float GetPercentageValue() => (float)currentValue / baseValue;
}
// Float base value

[System.Serializable]
public class BaseValueFloat : BaseValue<float>
{
    // empty constructor
    public BaseValueFloat() : base() { }
    // Constructor to set base value
    public BaseValueFloat(float baseValue) : base(baseValue) => this.baseValue = baseValue;

    // Add current value for float
    public void AddCurrentValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, 0, baseValue);
    }
    // returns 0 - 1
    public float GetPercentageValue() => currentValue / baseValue;
}