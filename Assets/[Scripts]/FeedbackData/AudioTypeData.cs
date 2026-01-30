using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptable/Event/Audio Type", fileName = "AudioType_")]
public class AudioTypeData : ScriptableObjectIndex
{
    public string audioTypeName;
    public AudioSource audioPrefeb;
    public AudioMixerGroup audioMixerGroup;
}
