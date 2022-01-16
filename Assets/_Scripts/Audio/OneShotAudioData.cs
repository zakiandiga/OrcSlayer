using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "NewOneShotAudio", menuName = "Data/Audio/OneShotAudioData")]
public class OneShotAudioData : ScriptableObject
{
    public string eventName;
    public EventReference eventPath;
}
