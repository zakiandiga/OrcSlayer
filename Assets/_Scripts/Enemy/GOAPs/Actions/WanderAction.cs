using UnityEngine;
using SGoap;

public class WanderAction : BasicAction
{
    private EnemyBehaviour enemy;

    private float targetPositionX;
    private Vector3 targetDestination;
    private float navDistanceTolerance;

    public override float CooldownTime => cooldownTime;
    private float cooldownTime = 0;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
    }

    public override bool PrePerform()
    {
        enemy.SetAlertColliderRadius(enemy.WanderAlertColliderRadius);
        targetPositionX = Random.Range(enemy.MinWanderRange, enemy.MaxWanderRange) * (Random.Range(0, 2) * 2 - 1);
        cooldownTime = Random.Range(0.7f, 2f); //Magic number

        targetDestination = new Vector3(enemy.EnemyPosition.x + targetPositionX, enemy.EnemyPosition.y, enemy.EnemyPosition.z);
        navDistanceTolerance = Random.Range(0.2f, 0.3f); //Magic number

        enemy.SetDestination(targetDestination, enemy.WanderSpeed);

        enemy.AnimManager.SetRunningBool(true);
        enemy.AnimManager.SetRunningFloat(0); //Magic number
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
            return EActionStatus.Running;     
    }

    public override bool PostPerform()
    {        
        enemy.AnimManager.SetRunningBool(false);
        if (!States.HasState("PatrolPointSpent"))
            States.AddState("PatrolPointSpent", 1);            
        else
            States.ModifyState("PatrolPointSpent", 1);            

        return base.PostPerform();
    }
}
