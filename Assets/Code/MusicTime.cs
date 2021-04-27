using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Music", menuName = "Muse/Music", order = 2), Serializable]
public class MusicTime : ScriptableObject
{
    public float time;
    public bool mute;
}
