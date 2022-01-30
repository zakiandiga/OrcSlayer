using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicManager : MonoBehaviour
{
    private EventInstance BGM1;
    [SerializeField] private EventReference music01;

    // Start is called before the first frame update
    void Start()
    {
        
        BGM1 = RuntimeManager.CreateInstance(music01);
        BGM1.start();
        BGM1.release();
    }

    private void OnDestroy()
    {
        BGM1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
