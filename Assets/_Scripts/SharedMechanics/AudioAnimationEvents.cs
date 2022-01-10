using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioAnimationEvents : MonoBehaviour
{
    public void PlaySoundOnLocation(OneShotAudioData eventAudio)
    {
        RuntimeManager.PlayOneShot(eventAudio.eventPath, this.transform.position);
    }

}
