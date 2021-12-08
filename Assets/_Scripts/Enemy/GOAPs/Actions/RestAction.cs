using UnityEngine;
using SGoap;

public class RestAction : BasicAction
{
    private EnemyBehaviour enemy;
    private float restingTime;
    private bool restOngoing;
    private string restingTimer = "RestingTimer";

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
    }

    public override EActionStatus Perform()
    {
        if(enemy.IsAlert())
        {
            Timer.ForceStopTimer(restingTimer);
            return EActionStatus.Failed;
        }

        if (restOngoing)
            return EActionStatus.Running;

        else
            return EActionStatus.Success;
    }

    public override bool PostPerform()
    {
        States.RemoveState("PatrolPointSpent");
        return base.PostPerform();        
    }

    public override bool PrePerform()
    {
        enemy.SetAlertColliderRadius(enemy.RestAlertColliderRadius);
        restingTime = Random.Range(5f, 8f);
        //StartCoroutine(RestingDelay());
        Debug.Log("Resting Timer Started");
        restOngoing = true;
        Timer.Create(RestingSwitch, restingTime, restingTimer);
        return base.PrePerform();
    }

    private void RestingSwitch()
    {
        Debug.Log("Resting Timer Done");
        if (restOngoing)
            restOngoing = false;
    }
}
