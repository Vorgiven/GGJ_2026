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

    int currentINdex;
    [SerializeField] List<RectTransform> InstructionList;
    [SerializeField] GameObject PrevBtn;
    [SerializeField] GameObject NextBtn;

    [SerializeField] FeedbackEventData e_curtainOpen;
    [SerializeField] FeedbackEventData e_curtainClose;
    [SerializeField] FeedbackEventData e_InstructionChangeSFX;
    private void Start()
    {
        foreach(var instructionRect in InstructionList)
        {
            instructionRect.gameObject.SetActive(false);
        }
    }
    public void ToggleCurtain(bool toogle)
    {
        UIImageVideoPlayer.Instance.SetImageTarget(targetImage);
        if (toogle)
        {
            ToggleIntructions(0);
            instructionGrp.interactable = false;
            instructionGrp.alpha = 0;
            UIImageVideoPlayer.Instance.Play(CLoseCUrtain, () => {
                instructionGrp.interactable = true;
                instructionGrp.DOFade(1, .03f).SetEase(Ease.InSine); });
            e_curtainClose?.InvokeEvent();
        }
        else
        {
            instructionGrp.interactable = false;
            instructionGrp.DOFade(0, .03f).SetEase(Ease.InSine);
            UIImageVideoPlayer.Instance.Play(OpenCurtain, () => { menuManager.ForceCloseMenu(instruictionMenu); });
            e_curtainOpen?.InvokeEvent();
        }
    }
    public void OnPressedInstructionButton(bool isNext)
    {
        e_InstructionChangeSFX?.InvokeEvent();
        RectTransform prevIns = InstructionList[currentINdex];
      
        PrevBtn.SetActive(true);
        NextBtn.SetActive(true);
        if (isNext)
        {
            currentINdex++;
           
        }
        else
        {
            currentINdex--;

        }

        RectTransform CurrentIns = InstructionList[currentINdex];
        CurrentIns.gameObject.SetActive(true);
        if (isNext)
        {
            CurrentIns.anchoredPosition = new Vector2(750 * 4, CurrentIns.anchoredPosition.y);
            CurrentIns.DOAnchorPosX(750,0.15f).SetEase(Ease.InSine);
            prevIns.DOAnchorPosX(750 * -4, 0.15f).SetEase(Ease.InSine).OnComplete(() => { prevIns.gameObject.SetActive(false); });
        }
        else
        {
            CurrentIns.anchoredPosition = new Vector2(750 * -4, CurrentIns.anchoredPosition.y);
            CurrentIns.DOAnchorPosX(750, 0.15f).SetEase(Ease.InSine);
            prevIns.DOAnchorPosX(750 * 4, 0.15f).SetEase(Ease.InSine).OnComplete(() => { prevIns.gameObject.SetActive(false); });
        }
        
    

        ToggleIntructions(currentINdex);
    }
    void ToggleIntructions(int index)
    {
        currentINdex = index;
        if (currentINdex == InstructionList.Count - 1)
        {
            NextBtn.SetActive(false);
        }
        else if (currentINdex == 0)
        {
            PrevBtn.SetActive(false);
        }
        InstructionList[index].gameObject.SetActive(true);
    }

}
