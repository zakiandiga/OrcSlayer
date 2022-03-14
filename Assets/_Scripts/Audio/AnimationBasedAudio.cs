using UnityEngine;
using FMODUnity;

public class AnimationBasedAudio : MonoBehaviour
{
    //Animation-based event audio
    public void PlaySoundOnLocation(OneShotAudioData eventAudio)
    {
        RuntimeManager.PlayOneShot(eventAudio.eventPath, this.transform.position);
    }

}
