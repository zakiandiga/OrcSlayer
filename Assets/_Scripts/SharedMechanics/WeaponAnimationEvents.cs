using System;
using UnityEngine;

public class WeaponAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private Weapon weaponComp;

    private WeaponCollider weaponCollider;

    [SerializeField] private ParticleSystem weaponTrail, weaponParticle, impactParticle;
    private float trailMaxLifetime = 0.5f; //Cheat
    private string trailTimer = "TrailTimer";

    public event Action<bool> OnAttackExecuting;

    private void Start()
    {
        weaponCollider = weapon.GetComponentInChildren<WeaponCollider>();
        weaponCollider.OnWeaponCollide += WeaponImpactEffect;
        weaponComp = weapon.GetComponent<Weapon>();

        if (weaponTrail != null && weaponTrail.isPlaying)
            weaponTrail.Stop();
    }

    private void OnDisable()
    {
        weaponCollider.OnWeaponCollide -= WeaponImpactEffect;
    }

    public void SetColliderOn()
    {
        weaponCollider.DamageCollider.enabled = true;
        //OnAttackExecuting?.Invoke(true);
    }

    public void SetColliderOff()
    {
        weaponCollider.DamageCollider.enabled = false;
        weaponCollider.DamageClear();

        OnAttackExecuting?.Invoke(false);
    }

    public void StartTrail()
    {
        if(weaponTrail != null)
        {
            weaponTrail.Play();

            /*
            if (Timer.TimerRunning(trailTimer))
                Timer.ForceStopTimer(trailTimer);
            
            Timer.Create(StopTrail, trailMaxLifetime, trailTimer);
            */
        }
    }

    private void WeaponImpactEffect(Vector3 position)
    {
        impactParticle.transform.position = position;
        Debug.Log("Particle player");
        impactParticle.Play();
    }

    public void StopTrail()
    {
        /*
        if (Timer.TimerRunning(trailTimer))
            Timer.ForceStopTimer(trailTimer);

        if (weaponTrail != null && weaponTrail.isPlaying)
            weaponTrail.Stop();
        */
    }

    private void PlayerTakesDamage(int damage) => ForceCancelEvent();

    private void PlayerLanding(Player player)
    {
        Debug.Log("Player Lands during air attack!!!!!!");
        ForceCancelEvent();
    }

    private void ForceCancelEvent()
    {
        OnAttackExecuting?.Invoke(false);

        if (weaponCollider.DamageCollider.enabled)
        {
            weaponCollider.DamageCollider.enabled = false;
            weaponCollider.DamageClear();
        }

        if (weaponTrail != null && weaponTrail.isPlaying)
            weaponTrail.Stop();
    }

}
