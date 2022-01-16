using UnityEngine;
using FMODUnity;

public class AnimationBasedAudio : MonoBehaviour
{
    //Animation-based event audio
    public void PlaySoundOnLocation(OneShotAudioData eventAudio)
    {
        RuntimeManager.PlayOneShot(eventAudio.eventPath, this.transform.position);
    }

    /*
    //Mechanic-based event audio
    private void WeaponImpact(Vector3 position, WeaponType weaponType)
    {
        //Switch to determine impactSound to play based on weaponType passed
        RuntimeManager.PlayOneShot(impactSound.eventPath, position);
        Debug.Log("Emmiter: " + this.gameObject.name);
    }
    */

}
