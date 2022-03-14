using UnityEngine;
using SGoap;

public class ChasePlayerAction : EnemyAction
{    
    private float targetPositionX;
    private Vector3 targetDestination;
    private float destinationTreshold; //same as navDistanceTolerance

    [SerializeField] private StringReference isInCombat;

    public override bool PrePerform()
    {
        enemy.SetAlertColliderRadius(enemy.AggroAlertColliderRadius);
        targetPositionX = enemy.currentPlayer.position.x;
        targetDestination = new Vector3(enemy.currentPlayer.position.x, enemy.EnemyPosition.y, enemy.EnemyPosition.z);
        destinationTreshold = Random.Range(actionData.minDestinationTreshold, actionData.maxDestinationTreshold);

        enemy.SetDestination(targetDestination, enemy.AggroSpeed);

        enemy.AnimManager.SetRunningBool(true);
        //enemy.AnimManager.SetRunningFloat(1);

        return base.PrePerform();
    }

    public override EActionStatus Perform()
    {
        if (!enemy.IsAlert())
        {
            Debug.Log("player GONE!!");
            //should add agent state confused (Design the actions lead to !confused)
            return EActionStatus.Failed;
        }

        if(enemy.NavRemainingDistance() <= destinationTreshold || enemy.AgentPath.status != UnityEngine.AI.NavMeshPathStatus.PathComplete)
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

        if (!States.HasState(isInCombat.Value))
            States.AddState(isInCombat.Value, 1);

        else
            States.SetState(isInCombat.Value, 1);

        return base.PostPerform();
    }
}
