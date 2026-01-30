using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/Event/E Data",fileName ="E_")]
public class FeedbackEventData : ScriptableObject
{
    public string feedbackName;
    public bool parented = true;
    public AudioTypeData audioType;
    public bool enableRandomPitch = false;
    public float minPitch = 1f;
    public float maxPitch = 1.1f;
    //public AUDIO_TYPE audioType;
    [SerializeField] private List<AudioInfo> audios = new List<AudioInfo>();
    public List<AudioInfo> Audios => audios;
    
    [Header("EFFECT SETTINGS")]
    [SerializeField] private List<EffectData> effects;
    public List<EffectData> Effects => effects;
    //[SerializeField] private bool effectSetParent; // set effect on position

    public System.Action eventToInvoke;
    Vector3 posData = Vector3.zero;
    Quaternion rotData = Quaternion.identity;
    Transform parentData = null;

    // Event invoking
    public void InvokeEvent() => eventToInvoke?.Invoke();
 
    public void InvokeEvent(Vector3 posData, Quaternion rotData, Transform parentData = null)
    {
        this.posData = posData;
        this.rotData = rotData;
        this.parentData = parentData;

        eventToInvoke?.Invoke();
    }

    public void PlayAudio(AudioSource audioSource)
    {
        audioSource.PlayOneShot(audios[Random.Range(0, audios.Count)].clip, audios[Random.Range(0, audios.Count)].volume);
    }


    // Getters
    public Vector3 GetPositionData() => posData;
    public Quaternion GetRotationData() => rotData;
    public Transform GetParentData() => parentData;

    // Events
    public void SubscribeEvent(System.Action action) => eventToInvoke += action;
    public void UnsubscribeEvent(System.Action action) => eventToInvoke -= action;
    public void UnsubscribeAll() => eventToInvoke = null;
}

[System.Serializable]
public class AudioInfo
{
    public AudioClip clip;
    public float volume = 1f;
}
