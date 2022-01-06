using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public Collider DamageCollider { get { return _damageCollider; } private set { } }

    private IDamageHandler damageHandler;

    private Collider _damageCollider;
    private List<GameObject> triggeredColliders = new List<GameObject>();

    private Vector3 contactPoint;

    public int DamageAmount { get; private set; }

    public event Action<Vector3> OnWeaponCollide;

    private void Start()
    {
        _damageCollider = GetComponent<Collider>();
        
    }

    public void DamageClear()
    {
        triggeredColliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (!triggeredColliders.Contains(other.gameObject) && other.transform.root != transform.root)
        {
            contactPoint = other.ClosestPointOnBounds(transform.position);
            OnWeaponCollide?.Invoke(contactPoint);
            triggeredColliders.Add(other.gameObject);

            //Debug.Log("collide with " + other.name);
            damageHandler = other.GetComponent<IDamageHandler>();
            if (damageHandler != null)
            {
                damageHandler.TakeDamage(1); //MagicNumber
            }
        }
        //else
            //Debug.Log("Already collided with " + other.gameObject);
        
    }
    
}
