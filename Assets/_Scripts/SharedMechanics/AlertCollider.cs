using System;
using System.Collections;
using UnityEngine;
using SGoap;

public class AlertCollider : MonoBehaviour
{
    private bool playerEnter;

    private EnemyBehaviour enemy;

    private IEnumerator playerExit;

    private void Start()
    { 
        enemy = GetComponentInParent<EnemyBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerEnter = true;
        enemy.SetPlayerOn(other.gameObject.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!playerEnter)
        {
            playerEnter = true;
            enemy.SetPlayerOn(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerEnter = false;
        enemy.SetPlayerOff(other.gameObject.transform);
    }


}
