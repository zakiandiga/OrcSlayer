using System;
using UnityEngine;

public class WeaponAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private Weapon weaponComp;

    private WeaponCollider weaponCollider;

    public event Action<bool> OnAttackDone;

    private void Start()
    {
        weaponCollider = weapon.GetComponentInChildren<WeaponCollider>();
        weaponComp = weapon.GetComponent<Weapon>();
    }

    public void SetColliderOn()
    {
        weaponCollider.DamageCollider.enabled = true;
    }

    public void SetColliderOff()
    {
        weaponCollider.DamageCollider.enabled = false;
        weaponCollider.DamageClear();

        OnAttackDone?.Invoke(true);
    }

    public void SetPhyOverlapOn()
    {
        weaponComp.AttackOn();
    }

    public void SetPhyOverLapOff()
    {
        weaponComp.AttackOff();
    }
}
