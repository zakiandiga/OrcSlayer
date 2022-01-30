using System;
using UnityEngine;

public interface IDamageHandler
{
    public event Action <int, Vector3, WeaponType> OnTakeDamage;
    public event Action<Vector3> OnDies;
    public event Action<Vector3> OnClearingCorpse;

    public void TakeDamage(int damage, Vector3 contactPoint, WeaponType weaponType);
    public void Die();    
}
