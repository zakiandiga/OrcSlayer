using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public NavMeshAgent agent { get; private set; }

    public int HealthPoint { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage, Vector3 contactPoint, WeaponType weaponType)
    {

    }

    public void Die()
    {

    }
}
