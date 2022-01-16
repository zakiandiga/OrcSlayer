using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using FMOD;
using System;

public class EventBasedAudio : MonoBehaviour
{
    private IDamageHandler damageHandler;
    //ScriptableObject eventData?
    [SerializeField] private OneShotAudioData impactSound;
    [SerializeField] private EventInstance impactInstance;

    private void Start()
    {
        damageHandler = GetComponent<IDamageHandler>();

        if (damageHandler != null)
            damageHandler.OnTakeDamage += PlayImpact;
    }

    private void PlayImpact(int damage, Vector3 impactPoint, WeaponType weaponType)
    {
        impactInstance = RuntimeManager.CreateInstance(impactSound.eventPath);
        impactInstance.setParameterByName("WeaponType", (float)weaponType, false);
        impactInstance.set3DAttributes(RuntimeUtils.To3DAttributes(impactPoint));
        impactInstance.start();
        impactInstance.release();
    }
}
