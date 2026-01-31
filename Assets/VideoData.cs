using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VideoData_", menuName = "Scriptable/Video Data")]
public class VideoData : ScriptableObject
{
    public List<Sprite> Frames = new List<Sprite>();

    public bool Loop = false;
}