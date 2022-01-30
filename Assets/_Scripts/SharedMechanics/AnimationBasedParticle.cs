using UnityEngine;

public class AnimationBasedParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftFeetStomp, rightFeetStomp, bodyFall;

    public void StompEffectL()
    {
        leftFeetStomp.Play();
    }

    public void StompEffectR()
    {
        rightFeetStomp.Play();
    }

    public void BodyFall()
    {
        bodyFall.Play();
    }
}
