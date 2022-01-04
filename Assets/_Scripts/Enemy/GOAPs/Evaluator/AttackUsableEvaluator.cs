using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGoap;

public class AttackUsableEvaluator : UsableEvaluator
{
    private EnemyBehaviour enemy;
    [SerializeField] bool isUsable;


    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
    }

    public override bool Evaluate(IContext context)
    {
        if(enemy.CurrentStamina > 5)
        {
            isUsable = true;
            return true;

        }

        isUsable = false;
        return false;

    }
}
