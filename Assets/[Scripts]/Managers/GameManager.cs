using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private BaseValueInt healthValue = new BaseValueInt(100);
    [Header("UI")]
    [SerializeField] private Image imgHealthBar;
    [Header("Feedback")]
    [SerializeField] private FeedbackEventData e_correct;
    [SerializeField] private FeedbackEventData e_wrong;

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
    // Scoring system
    public void CorrectMask()
    {
        Debug.Log("NICE!");
        e_correct?.InvokeEvent();
    }
    public void WrongMask()
    {
        DeductHealth(10);
        e_wrong?.InvokeEvent();
    }
}
