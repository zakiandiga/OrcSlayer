using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationEvents : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftFeetStomp, rightFeetStomp;


    public void StompEffectL()
    {
        leftFeetStomp.Play();
    }

    public void StompEffectR()
    {
        rightFeetStomp.Play();
    }
}
