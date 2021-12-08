using UnityEngine;
using SGoap;

public class ChasePlayerAction : BasicAction
{    
    private EnemyBehaviour enemy;

    private float targetPositionX;
    private Vector3 targetDestination;
    private float hitRange; //same as navDistanceTolerance

    private string spinAttackReadyTimer = "SpinAttackReadying";

    void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
    }

    public override bool PrePerform()
    {
        enemy.SetAlertColliderRadius(enemy.AggroAlertColliderRadius);
        targetPositionX = enemy.Player.position.x;
        targetDestination = new Vector3(enemy.Player.position.x, enemy.EnemyPosition.y, enemy.EnemyPosition.z);
        hitRange = Random.Range(2f, 2.5f);

        enemy.SetDestination(targetDestination, enemy.AggroSpeed);

        enemy.AnimManager.SetRunningBool(true);
        enemy.AnimManager.SetRunningFloat(1);

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

        if(enemy.NavRemainingDistance() <= hitRange || enemy.AgentPath.status != UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            //Debug.Log("if player is still at the hitting range at this point, ATTACK, else, chase again!");
            enemy.SetStopNavMesh();
            return EActionStatus.Success;
        }

        else
            return EActionStatus.Running;
    }

    public override bool PostPerform()
    {
        enemy.AnimManager.SetRunningBool(false);

        return base.PostPerform();
    }
}
