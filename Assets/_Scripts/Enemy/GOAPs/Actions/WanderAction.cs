using UnityEngine;
using SGoap;

public class WanderAction : EnemyAction
{
    private float targetPositionX;
    private Vector3 targetDestination;
    private float navDistanceTolerance;
        
    public override float CooldownTime => cooldownTime;
    private float cooldownTime = 0;

    [SerializeField] private StringReference patrolPoint;

    public override bool PrePerform()
    {
        enemy.SetAlertColliderRadius(enemy.WanderAlertColliderRadius);
        targetPositionX = Random.Range(enemy.MinWanderRange, enemy.MaxWanderRange) * (Random.Range(0, 2) * 2 - 1);
        cooldownTime = Random.Range(actionData.cooldownMinTimeModifier, actionData.cooldownMaxTimeModifier);

        targetDestination = new Vector3(enemy.EnemyPosition.x + targetPositionX, enemy.EnemyPosition.y, enemy.EnemyPosition.z);
        navDistanceTolerance = Random.Range(actionData.minDestinationTreshold, actionData.maxDestinationTreshold);

        enemy.SetDestination(targetDestination, actionData.moveSpeed);

        enemy.AnimManager.SetRunningBool(true);

        return base.PrePerform();
    }

    public override EActionStatus Perform()
    {  
        if(enemy.IsAlert())
            return EActionStatus.Failed;

        if (enemy.NavRemainingDistance() <= navDistanceTolerance || enemy.AgentPath.status != UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            enemy.SetStopNavMesh();
            return EActionStatus.Success;
        }

        else
        {
            enemy.AnimManager.SetRunningFloat(enemy.NavVelocity());
            return EActionStatus.Running;     
        }
    }

    public override bool PostPerform()
    {        
        enemy.AnimManager.SetRunningBool(false);
        if (!States.HasState(patrolPoint.Value))
            States.AddState(patrolPoint.Value, 5);            
        else
            States.ModifyState(patrolPoint.Value, -1);            

        return base.PostPerform();
    }
}
