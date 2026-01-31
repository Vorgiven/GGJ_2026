using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] Canvas EndGameCanvas;
    [SerializeField] Image TargetImage;
    [SerializeField] VideoData endGameVideoData;
    [SerializeField] FeedbackEventData endGameSFX;
    [SerializeField] FeedbackEventData Drumroll;
    [SerializeField] FeedbackEventData Show1;
    [SerializeField] FeedbackEventData Show2;

    [SerializeField] GameManager gameManager;
    [Header("References")]
    [SerializeField] CanvasGroup scoreGrp;
    [SerializeField] TMP_Text score;
    [SerializeField] CanvasGroup comboGrp;
    [SerializeField] TMP_Text combo;
    [SerializeField] CanvasGroup mainMenuBtn;
    [SerializeField] AudioSource musicPlayer;

    bool gameHasEnded;
    private void Start()
    {
        ToggleEndGame(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameHasEnded)
        {
            ToggleEndGame(true);
        }
    }
    public void ToggleEndGame(bool toggle)
    {
        gameHasEnded = toggle;
        EndGameCanvas.gameObject.SetActive(toggle);
        scoreGrp.alpha = 0;
        comboGrp.alpha = 0;
        mainMenuBtn.alpha = 0;

        if (toggle)
        {
            musicPlayer.DOPitch(0, .25f).SetEase(Ease.InCubic).SetUpdate(true).OnComplete(() => {
                Time.timeScale = 0;
                score.text = gameManager.Score.ToString();
                combo.text = gameManager.HightestCombo.ToString();
                endGameSFX?.InvokeEvent();
                UIImageVideoPlayer.Instance.SetImageTarget(TargetImage);
                UIImageVideoPlayer.Instance.Play(endGameVideoData, false);
                DOVirtual.DelayedCall(1.35f + .3f, () => {
                    Drumroll?.InvokeEvent();
                    DOVirtual.DelayedCall(1, () =>
                    {
                        Show1?.InvokeEvent();
                        scoreGrp.alpha = 1;
                        DOVirtual.DelayedCall(0.5f, () =>
                        {
                            comboGrp.alpha = 1;
                            Show2?.InvokeEvent();
                            DOVirtual.DelayedCall(1.05f, () =>
                            {
                                mainMenuBtn.alpha = 1;

                            });
                        });
                    });
                });
            });
            
            
        }
        else
        {
            Time.timeScale = 1;
        }

    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main menu");
    }
}
