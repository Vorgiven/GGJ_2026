using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIImageVideoPlayer : MonoBehaviour
{
    public static UIImageVideoPlayer Instance { get; private set; }

    public static event Action OnVideoFinished;

    [Header("References")]
    [SerializeField] private Image targetImage;

    [Header("Playback Settings")]
    [SerializeField] private float frameRate = 30f;

    private Coroutine playRoutine;
    private VideoData currentVideo;
    private int currentFrame;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void SetImageTarget(Image _newImage)
    {
        targetImage = _newImage;
    }
    public void Play(VideoData videoData, bool timeScale=true, System.Action OnVideoEndFunction = null)
    {
        if (!targetImage.gameObject.activeSelf)
        {
            targetImage.gameObject.SetActive(true);
        }

        if (videoData == null || videoData.Frames.Count == 0)
        {
            Debug.LogWarning("UIImageVideoPlayer: VideoData is null or empty.");
            return;
        }

        Stop();

        currentVideo = videoData;
        currentFrame = 0;

        playRoutine = StartCoroutine(PlayRoutine(timeScale,OnVideoEndFunction));
    }


    public void Stop()
    {
        if (playRoutine != null)
        {
            StopCoroutine(playRoutine);
            playRoutine = null;
        }

        currentVideo = null;
        currentFrame = 0;
    }

    private IEnumerator PlayRoutine(bool _usesTimeScale, System.Action OnVidEnd)
    {
        float frameTime = 1f / frameRate;

        while (true)
        {
            targetImage.sprite = currentVideo.Frames[currentFrame];
            currentFrame++;

            if (currentFrame >= currentVideo.Frames.Count)
            {
                if (currentVideo.Loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    playRoutine = null;
                    OnVideoFinished?.Invoke();
                    OnVidEnd?.Invoke();
                    yield break;
                }
            }

            if (_usesTimeScale)
                yield return new WaitForSeconds(frameTime);
            else
                yield return new WaitForSecondsRealtime(frameTime);
        }
    }
}
