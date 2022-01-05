using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationEvents : MonoBehaviour
{
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private ParticleSystem leftFeetStomp, rightFeetStomp;

    // Start is called before the first frame update
    public void StompEffectL()
    {
        leftFeetStomp.Play();
    }

    public void StompEffectR()
    {
        rightFeetStomp.Play();
    }
}
