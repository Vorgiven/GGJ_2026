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
    [SerializeField] private int score = 0;
    [SerializeField] private int currentCombo = 0;
    [SerializeField] private int comboToActivate = 3;
    [SerializeField] private int highestCombo = 0;
    [SerializeField] private TimerCheck timerCombo;
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
        currentCombo++;


        int comboScoreToAdd = 0;
        // Check if eligible for combo
        if(currentCombo>= comboToActivate)
        {
            float comboMultiplier = 1;
            comboScoreToAdd = (int)(comboMultiplier * 1);
        }

        // Highest Combo
        if(currentCombo > highestCombo)
        {
            highestCombo = currentCombo;
        }

        // Add score
        score += 10 * comboScoreToAdd;
    }
    public void WrongMask()
    {
        DeductHealth(10);
        e_wrong?.InvokeEvent();
        ResetCombo();
    }

    public void ResetCombo()
    {
        currentCombo = 0;
    }
}
