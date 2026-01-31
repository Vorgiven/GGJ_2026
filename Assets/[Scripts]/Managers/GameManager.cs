using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //[SerializeField] private List<MaskGroupData> maskGroupUnlocks = new List<MaskGroupData>();
    [SerializeField] private List<MasskGroupUnlockInfo> maskGroupinfos = new List<MasskGroupUnlockInfo>();

    [SerializeField] private BaseValueInt healthValue = new BaseValueInt(100);
    [SerializeField] private int score = 0;
    [SerializeField] private int currentCombo = 0;
    [SerializeField] private int comboToActivate = 3;
    [SerializeField] private int highestCombo = 0;
    [SerializeField] private TimerCheck timerCombo;
    [Header("UI")]
    [SerializeField] private RectTransform uiHealth;
    [SerializeField] private Image imgHealthBar;
    [SerializeField] private GameObject uiCombo;
    [SerializeField] private Image imgComboBar;
    [SerializeField] private TMP_Text txtCombo;
    [SerializeField] private TMP_Text txtScore;
    [Header("Feedback")]
    [SerializeField] private FeedbackEventData e_correct;
    [SerializeField] private FeedbackEventData e_wrong;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        timerCombo.SetAutoResetTimer(false);

        imgHealthBar.fillAmount = 1;
        healthValue.Initialize();

        foreach (var maskGroupinfo in maskGroupinfos)
        {
            maskGroupinfo.Initialize();
        }
    }
    private void Start()
    {
        txtScore.text = score.ToString();
    }
    private void Update()
    {
        if(timerCombo.UpdateTimer(currentCombo>0))
        {
            ResetCombo();
        }
        imgComboBar.fillAmount = 1-timerCombo.GetPercentage();

    }

    public void DeductHealth(int amt)
    {
        healthValue.AddCurrentValue(-amt);
        imgHealthBar.fillAmount = healthValue.GetPercentageValue();
        uiHealth.DOShakeAnchorPos(0.15f, new Vector2(8f, 8f),20);
    }
    public void AddHealth(int amt)
    {
        healthValue.AddCurrentValue(amt);
        imgHealthBar.fillAmount = healthValue.GetPercentageValue();
    }
    // Scoring system
    public void CorrectMask(Enemy enemyTarget)
    {
        currentCombo++;

        int comboScoreToAdd = 0;
        // Check if eligible for combo
        if(currentCombo>= comboToActivate)
        {
            float comboMultiplier = 1;
            comboScoreToAdd = (int)(comboMultiplier * 1);
            uiCombo.gameObject.SetActive(true);
            timerCombo.ResetTimer();
            txtCombo.text = currentCombo + "x\nCombo";
        }

        // Highest Combo
        if (currentCombo > highestCombo)
        {
            highestCombo = currentCombo;
        }

        // Add score
        score += 10 + comboScoreToAdd;
        txtScore.text = score.ToString();

        e_correct?.InvokeEvent(enemyTarget.transform.position,Quaternion.identity,enemyTarget.transform);
    }
    public void WrongMask(Enemy enemyTarget)
    {
        DeductHealth(10);
        ResetCombo();
        e_wrong?.InvokeEvent(enemyTarget.transform.position, Quaternion.identity, enemyTarget.transform);
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        timerCombo.ResetTimer();
        uiCombo.gameObject.SetActive(false);
    }

    public List<MasskGroupUnlockInfo> GetMaskGroupInfos() => maskGroupinfos;
    public void UnlockMaskGroup(MaskGroupData maskGroupUnlock)
    {
        MasskGroupUnlockInfo masskGroupUnlockInfo = GetMaskGroupInfos().Find((e) => e.maskGroupData == maskGroupUnlock);
        if (masskGroupUnlockInfo != null)
        {
            masskGroupUnlockInfo.Unlock();
        }
    }
}

[System.Serializable]
public class MasskGroupUnlockInfo
{
    public MaskGroupData maskGroupData;
    public MaskGroup maskGroupDrawer;
    public GameObject masGroupkSprite;
    [SerializeField]private bool unlock = true;

    public void Initialize()
    {
        if (unlock)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }
    public void Unlock()
    {
        unlock = true;
        maskGroupDrawer.gameObject.SetActive(true);
        maskGroupDrawer.transform.parent.GetComponent<MaskGroupSlot>().CurrentlyEquippedMaskGrp = maskGroupDrawer;
             
        masGroupkSprite.gameObject.SetActive(true);
    }
    public void Lock()
    {
        unlock = false;
        maskGroupDrawer.gameObject.SetActive(false);
        masGroupkSprite.gameObject.SetActive(false);
    }
}