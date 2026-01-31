using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] Image targetVidImage;
    [SerializeField] VideoData ShowCreditsVideoData;
    [SerializeField] VideoData HideCreditsVideoData;

    [SerializeField] CanvasGroup buttonCanvasGrp;
    [SerializeField] CanvasGroup CreditsGrp;
    [SerializeField] MenuManager menuManger;

    [SerializeField] FeedbackEventData e_ShowCredits;
    [SerializeField] FeedbackEventData e_HideCredits;
    public void ToggleCredits(bool toogle)
    {
        UIImageVideoPlayer.Instance.SetImageTarget(targetVidImage);
        if(toogle)
        {
            e_ShowCredits?.InvokeEvent();
            buttonCanvasGrp.interactable = false;
            buttonCanvasGrp.DOFade(0, 0);
            UIImageVideoPlayer.Instance.Play(ShowCreditsVideoData, () => { buttonCanvasGrp.interactable = true; 
                buttonCanvasGrp.DOFade(1, .15f).SetEase(Ease.InCubic); });
        }
        else
        {
            e_HideCredits?.InvokeEvent();
            buttonCanvasGrp.interactable = false;
            buttonCanvasGrp.DOFade(0, .05f).SetEase(Ease.InCubic);
            UIImageVideoPlayer.Instance.Play(HideCreditsVideoData, () => { menuManger.ForceCloseMenu(CreditsGrp); });
        }
    }
}
