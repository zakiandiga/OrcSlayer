using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBox : MonoBehaviour, IDamageHandler
{
    public void Die()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Box hit " + damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The box exist!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Box Collide with " + other.gameObject.name);
    }
}