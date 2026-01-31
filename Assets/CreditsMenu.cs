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
    [SerializeField] RectTransform creditsText;
    public void ToggleCredits(bool toogle)
    {
        UIImageVideoPlayer.Instance.SetImageTarget(targetVidImage);
        if(toogle)
        {
            buttonCanvasGrp.DOKill();
            creditsText.DOKill();
            creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, 770);

            e_ShowCredits?.InvokeEvent();
            buttonCanvasGrp.interactable = false;
            buttonCanvasGrp.DOFade(0, 0);

            DOVirtual.DelayedCall(0.3667f / 22 * 4, () => {
                creditsText.DOAnchorPosY(-120, 0.3667f / 22 * 3).SetEase(Ease.InSine).OnComplete(() => {
                    creditsText.DOAnchorPosY(-30, 0.3667f / 22 * 5).SetEase(Ease.OutSine).OnComplete(() => {
                        creditsText.DOAnchorPosY(-50, 0.3667f / 22 * 10).SetEase(Ease.InSine);
                    });
                });
            });
            
            UIImageVideoPlayer.Instance.Play(ShowCreditsVideoData,true, () => { buttonCanvasGrp.interactable = true; 
                buttonCanvasGrp.DOFade(1, .15f).SetEase(Ease.InCubic); });
        }
        else
        {
            buttonCanvasGrp.DOKill();
            creditsText.DOKill();
            creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, -50);

            e_HideCredits?.InvokeEvent();
            buttonCanvasGrp.interactable = false;
            creditsText.DOAnchorPosY(770, 0.3667f).SetEase(Ease.OutSine);
            buttonCanvasGrp.DOFade(0, .05f).SetEase(Ease.InCubic);
            UIImageVideoPlayer.Instance.Play(HideCreditsVideoData, true,() => { menuManger.ForceCloseMenu(CreditsGrp); });
        }
    }
}
