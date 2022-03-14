using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public Collider DamageCollider { get { return _damageCollider; } private set { } }

    private IDamageHandler damageHandler;
    private IAttackHandler attackHandler;

    [SerializeField] private WeaponType _weaponType;

    private Collider _damageCollider;
    private List<GameObject> triggeredColliders = new List<GameObject>();

    private int currentDamage;
    private Vector3 contactPoint;

    private void Start()
    {
        _damageCollider = GetComponent<Collider>();
        attackHandler = transform.root.GetComponent<IAttackHandler>();        
    }


    public void DamageClear()
    {
        triggeredColliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackHandler != null)
            currentDamage = attackHandler.GetCurrentDamage();

        if (!triggeredColliders.Contains(other.gameObject) && other.transform.root != transform.root)
        {
            contactPoint = other.ClosestPointOnBounds(transform.position);
            
            triggeredColliders.Add(other.gameObject);

            //Debug.Log("collide with " + other.name);
            damageHandler = other.GetComponent<IDamageHandler>();

            if (damageHandler != null)
                damageHandler.TakeDamage(currentDamage, contactPoint, _weaponType);

        }        
    }
    
}

public enum WeaponType
{
    slashing,
    piercing,
    blunt,
    block,
    none,
}
