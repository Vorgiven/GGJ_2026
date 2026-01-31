using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private BaseValueInt healthValue = new BaseValueInt(100);

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DeductHealth(int amt)
    {
        healthValue.AddCurrentValue(-amt);
    }
}
