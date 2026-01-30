using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Tools/Min Max/Bool", fileName = "MinMax_Bool_")]
public class MinMaxValueBoolData : ScriptableObject
{
    public MinMaxValueBool minMaxValue = new MinMaxValueBool(true,false);
}