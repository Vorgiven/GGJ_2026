using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class InstructionMenuManager : MonoBehaviour
{
    [SerializeField] VideoData CLoseCUrtain;
    [SerializeField] VideoData OpenCurtain;
    [SerializeField] CanvasGroup instructionGrp;
    [SerializeField] MenuManager menuManager;
    [SerializeField] CanvasGroup instruictionMenu;

    [SerializeField] FeedbackEventData e_curtainOpen;
    [SerializeField] FeedbackEventData e_curtainClose;
    bool isClose;
    public void ToggleCurtain(bool toogle)
    {
        isClose = toogle;
        if (toogle)
        {
            instructionGrp.alpha = 0;
            UIImageVideoPlayer.Instance.Play(CLoseCUrtain);
            e_curtainClose?.InvokeEvent();
        }
        else
        {
            instructionGrp.DOFade(0, .03f).SetEase(Ease.InSine);
            UIImageVideoPlayer.Instance.Play(OpenCurtain);
            e_curtainOpen?.InvokeEvent();
        }
    }
    void OnCurrtainAnimationFinsihed()
    {
        if(isClose)
        {
            instructionGrp.DOFade(1, .03f).SetEase(Ease.InSine);
        }
        else
        {
            menuManager.ForceCloseMenu(instruictionMenu);
        }
    }
    private void OnEnable()
    {
        UIImageVideoPlayer.OnVideoFinished += OnCurrtainAnimationFinsihed;
    }
    private void OnDisable()
    {
        UIImageVideoPlayer.OnVideoFinished -= OnCurrtainAnimationFinsihed;
    }
}
