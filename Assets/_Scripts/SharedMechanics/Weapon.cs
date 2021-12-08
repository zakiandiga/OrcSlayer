using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//Attach this to weapon object
public class Weapon : MonoBehaviour
{
    private int _currentDamage;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _radius;
    

    [SerializeField] private LayerMask collisionLayer;

    private Collider[] hitColliders;

    private bool isAttacking = false;

    private List<Collider> detectedCollider = new List<Collider>();
    public void SetAttack()
    {
        hitColliders = Physics.OverlapCapsule(_startPoint.position, _endPoint.position, _radius, collisionLayer);
              
        foreach (Collider hitCollider in hitColliders)
        {            
            Debug.Log("hit " + hitCollider.name);
            if(!detectedCollider.Contains(hitCollider))
            {
                detectedCollider.Add(hitCollider);
                AttackExecute(hitCollider);
            }
        }
    }

    private void AttackExecute(Collider col)
    {
        Debug.Log("attacked " + col.gameObject.name);
    }

    public void AttackOn()
    {
        isAttacking = true;
    }

    public void AttackOff()
    {
        isAttacking = false;
    }

    private void FixedUpdate()
    {
        if(isAttacking)
        {
            SetAttack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_startPoint.position, _radius);
        Gizmos.DrawWireSphere(_endPoint.position, _radius);
    }

}
