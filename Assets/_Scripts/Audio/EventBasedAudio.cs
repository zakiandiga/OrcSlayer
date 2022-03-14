using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class EventBasedAudio : MonoBehaviour
{
    private IDamageHandler damageHandler;
    //ScriptableObject eventData?
    [SerializeField] private OneShotAudioData impactSound;
    [SerializeField] private OneShotAudioData dieSound;
    [SerializeField] private EventInstance impactInstance;

    private void Start()
    {
        damageHandler = GetComponent<IDamageHandler>();

        if (damageHandler != null)
        {
            damageHandler.OnTakeDamage += PlayImpact;
            damageHandler.OnDies += PlayDies;
        }
    }

    private void OnDisable()
    {
        if(damageHandler != null)
        {
            damageHandler.OnTakeDamage -= PlayImpact;
            damageHandler.OnDies -= PlayDies;
        }
    }

    private void PlayImpact(int damage, Vector3 impactPoint, WeaponType weaponType)
    {
        impactInstance = RuntimeManager.CreateInstance(impactSound.eventPath);
        impactInstance.setParameterByName("WeaponType", (float)weaponType, false);
        impactInstance.set3DAttributes(RuntimeUtils.To3DAttributes(impactPoint));
        impactInstance.start();
        impactInstance.release();
    }

    private void PlayDies(GameObject agent, Vector3 position)
    {
        impactInstance = RuntimeManager.CreateInstance(dieSound.eventPath);
        impactInstance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        impactInstance.start();
        impactInstance.release();
    }
}
