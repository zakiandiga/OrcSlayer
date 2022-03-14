using System;
using UnityEngine;

public class WeaponAnimationEvents : MonoBehaviour
{
    public int CurrentDamage { get { return _currentDamage; } private set { } }

    [SerializeField] private GameObject weapon;

    private WeaponCollider weaponCollider;
    private int _currentDamage;

    [SerializeField] private ParticleSystem weaponTrail, weaponParticle, impactParticle;
    private float trailMaxLifetime = 0.5f; //Cheat
    private string trailTimer = "TrailTimer";

    public event Action<bool> OnAttackExecuting;

    private void Start()
    {
        weaponCollider = weapon.GetComponentInChildren<WeaponCollider>();

        if (weaponTrail != null && weaponTrail.isPlaying)
            weaponTrail.Stop();
    }

    public void SetColliderOn()
    {
        weaponCollider.DamageCollider.enabled = true;
    }

    public void SetColliderOff()
    {
        weaponCollider.DamageCollider.enabled = false;
        weaponCollider.DamageClear();

        OnAttackExecuting?.Invoke(false);
    }

    public void SetCurrentDamage(int damage)
    {
        _currentDamage = damage;
    }

    public void StartTrail()
    {
        if(weaponTrail != null)
        {
            weaponTrail.Play();
        }
    }


    private void PlayerTakesDamage(int damage) => ForceCancelEvent();

    private void PlayerLanding(Player player)
    {
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


