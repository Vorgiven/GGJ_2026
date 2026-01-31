using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameFeedback : MonoBehaviour
{
    public static GameFeedback Instance;

    [SerializeField] private List<FeedbackEventData> feedbackDatas = new List<FeedbackEventData>();
    [Header("AUDIO POOLING")]
    public AudioTypeData defaultAudioType;
    private List<AudioTypeData> audioTypeDatas = new List<AudioTypeData>();
    private List<ObjectPool<GameObject>> AudioPooling = new List<ObjectPool<GameObject>>();
    [Header("EFFECT POOLING")]
    private List<EffectData> effectDatas = new List<EffectData>();
    private List<ObjectPool<GameObject>> effectsPooling = new List<ObjectPool<GameObject>>();
    private bool isLoaded = false;
    public bool IsLoaded => isLoaded;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    private void Start()
    {
        for (int i = 0; i < feedbackDatas.Count; i++)
        {
            FeedbackEventData feedbackDataTemp = feedbackDatas[i];
            // Add audio type
            AudioTypeData audioTypeNew = audioTypeDatas.Find((e) => e == feedbackDataTemp.audioType);
            if (audioTypeNew == null)
                audioTypeDatas.Add(feedbackDataTemp.audioType);

            // Add effects
            foreach (var effect in feedbackDataTemp.Effects)
            {
                if (effect == null) continue; // if no effect
                EffectData effectDataNew = effectDatas.Find((e) => e == effect);
                if (effectDataNew == null)
                    effectDatas.Add(effect);
            }
        }

        // Add audio to pool
        for (int i = 0; i < audioTypeDatas.Count; i++)
        {
            AudioTypeData audioTypeData = audioTypeDatas[i];
            audioTypeData.SetIndex(i);
            ObjectPoolGO audioTypePool = new ObjectPoolGO();
            audioTypePool.obj = audioTypeData.audioPrefeb.gameObject;
            AudioPooling.Add(audioTypePool);
        }

        // Add effects to pool and set its index
        for (int i = 0; i < effectDatas.Count; i++)
        {
            EffectData effectData = effectDatas[i];
            effectData.SetIndex(i);
            ObjectPoolGO effectPool = new ObjectPoolGO();
            effectPool.obj = effectData.effectObject;
            effectsPooling.Add(effectPool);
        }


        // Set audio to its Audio Mixer Group
        for (int i = 0; i < AudioPooling.Count; i++)
        {
            ObjectPool<GameObject> audioPool = AudioPooling[i];
            int n = i;
            audioPool.SubscribeEventObjectSpawned((poolObj) => {
                poolObj.GetComponent<AudioSource>().outputAudioMixerGroup = audioTypeDatas[n].audioMixerGroup;
            });
        }

        // Initialize pool
        foreach (ObjectPool<GameObject> audioP in AudioPooling)
            audioP.Initialize();
        foreach (ObjectPool<GameObject> effectP in effectsPooling)
            effectP.Initialize();

    
        // Subscribe events for feedback datas
        foreach (FeedbackEventData feedback in feedbackDatas)
        {
            feedback.SubscribeEvent(() => Callback(feedback));
        }
        isLoaded = true;
    }


    private void OnDestroy()
    {
        foreach (var feedback in feedbackDatas)
        {
            feedback.UnsubscribeAll();
        }
    }
    void Callback(FeedbackEventData data)
    {
        //Debug.Log(data.feedbackName + " Called");
        // Store the nessecery data in variable
        List<AudioInfo> audioClips = data.Audios;
        int maxAudioClip = data.Audios.Count;
        // Play audio
        if (maxAudioClip > 0)
        {
            GameObject audioObj = AudioPooling[data.audioType.GetIndex()].GetObj(data.GetPositionData(), data.GetRotationData(), data.parented ? data.GetParentData() : null);
            // Get the audio source
            AudioSource audioS = audioObj.GetComponent<AudioSource>();

            if (data.enableRandomPitch)
                audioS.pitch = UnityEngine.Random.Range(data.minPitch, data.maxPitch);

            audioS.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Count)].clip, audioClips[UnityEngine.Random.Range(0, audioClips.Count)].volume);

            StartCoroutine(IeDisableAudio(audioS));
        }
        //foreach (var audio in data.Audios)
        //{
        //    GameObject audioObj = AudioPooling[data.audioType.GetIndex()].GetObj(data.GetPositionData(), data.GetRotationData(), data.parented ? data.GetParentData() : null);
        //    // Get the audio source
        //    AudioSource audioS = audioObj.GetComponent<AudioSource>();

        //    //// play audio at random
        //    //audioS.PlayOneShot(audioClips[UnityEngine.Random.Range(0, maxAudioClip)]);
        //    //audioS.Play()

        //    //data.PlayAudio(audioS);
        //}
        ////}

        // Spawn effects
        foreach (var effect in data.Effects)
        {
            //EffectData effectDataNew = eff.Find((e) => e == effect);
            GameObject effectObj = effectsPooling[effect.GetIndex()].GetObj(data.GetPositionData(), data.GetRotationData(), data.parented ? data.GetParentData() : null);
            effectObj.transform.localScale = Vector3.one;
        }

    }

    //public ObjectPool[] GetAudioPools()
    //{
    //    return AudioPooling.ToArray();
    //}

    //public ObjectPool[] GetEffectPools()
    //{
    //    return effectsPooling.ToArray();
    //}
    private IEnumerator IeDisableAudio(AudioSource audioSource)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        audioSource.gameObject.SetActive(false);
    }
}