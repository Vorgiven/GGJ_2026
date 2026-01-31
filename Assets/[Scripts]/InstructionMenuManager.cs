using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class InstructionMenuManager : MonoBehaviour
{
    [SerializeField] Image targetImage;

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
        UIImageVideoPlayer.Instance.SetImageTarget(targetImage);
        isClose = toogle;
        if (toogle)
        {
            instructionGrp.alpha = 0;
            UIImageVideoPlayer.Instance.Play(CLoseCUrtain, () => { instructionGrp.DOFade(1, .03f).SetEase(Ease.InSine); });
            e_curtainClose?.InvokeEvent();
        }
        else
        {
            instructionGrp.DOFade(0, .03f).SetEase(Ease.InSine);
            UIImageVideoPlayer.Instance.Play(OpenCurtain, () => { menuManager.ForceCloseMenu(instruictionMenu); });
            e_curtainOpen?.InvokeEvent();
        }
    }
}
