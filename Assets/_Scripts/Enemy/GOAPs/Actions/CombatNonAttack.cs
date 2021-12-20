using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGoap;

public class CombatNonAttack : EnemyAction
{
    private AdjustmentType adjustmentType; // = (AdjustmentType).index;
    private int adjustmentTypeIndex;

    private float targetDestinationX;
    private Vector3 targetDestination;
    private float waitDuration = 3f;

    private float destinationTreshold;

    private string tauntingTimer = "TauntingTimer";

    private bool adjustmentOngoing = false;

    [SerializeField] private StringReference isInCombat;

    public override EActionStatus Perform()
    {
        // failed condition: staggered, dies, etc
        if(!enemy.IsAlert())
        {
            States.RemoveState(isInCombat.Value);
            return EActionStatus.Failed;
        }

        if (adjustmentType == AdjustmentType.move && (enemy.NavRemainingDistance() <= destinationTreshold || enemy.AgentPath.status != UnityEngine.AI.NavMeshPathStatus.PathComplete))
        {
            enemy.SetStopNavMesh();
            enemy.AnimManager.SetRunningBool(false);
            return EActionStatus.Success;
        }
        else if (adjustmentType == AdjustmentType.stay && !adjustmentOngoing)
            return EActionStatus.Success;

        else
        {
            enemy.AnimManager.SetRunningFloat(enemy.NavVelocity());
            return EActionStatus.Running;
        }
    }

    public override bool PostPerform()
    {

        return base.PostPerform();
    }

    public override bool PrePerform()
    {
        adjustmentTypeIndex = Random.Range(0,2);
        adjustmentType = (AdjustmentType)adjustmentTypeIndex;
        adjustmentOngoing = true;

        if(adjustmentType == AdjustmentType.stay) //stay
        {
            Debug.Log("Taunting");
            enemy.SetLookAt(transform.position);
            enemy.AnimManager.SetTaunt();
            Timer.Create(TauntingSwitch, waitDuration, tauntingTimer);
        }
        else if(adjustmentType == AdjustmentType.move)
        {
            Debug.Log("Move a bit");
            
            targetDestinationX = enemy.Player.position.x;

            targetDestination = new Vector3(targetDestinationX, enemy.EnemyPosition.y, enemy.EnemyPosition.z);

            destinationTreshold = Random.Range(actionData.minDestinationTreshold, actionData.maxDestinationTreshold);

            enemy.SetDestination(targetDestination, actionData.moveSpeed);

            enemy.AnimManager.SetRunningBool(true);
            //enemy.AnimManager.SetRunningFloat(0.5f);
        }
        return base.PrePerform();
    }

    private void TauntingSwitch()
    {
        if (adjustmentOngoing)
            adjustmentOngoing = false;
    }
}

public enum AdjustmentType
{
    stay,
    move
}