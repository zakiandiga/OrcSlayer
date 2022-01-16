using System;
using UnityEngine;

public interface IDamageHandler
{
    public event Action <int, Vector3, WeaponType> OnTakeDamage;

    public void TakeDamage(int damage, Vector3 contactPoint, WeaponType weaponType);
    public void Die();    
}
