using UnityEngine;
using SGoap;

public class BasicAttack : EnemyAction
{
    private bool hitOngoing = false;
    private int delayTime = 0;

    public override float CooldownTime => 
        Random.Range(actionData.cooldownMinTimeModifier, actionData.cooldownMaxTimeModifier); 


    public override void DynamicallyEvaluateCost()
    {
        Cost = Random.Range(1, actionData.chanceValue);
    }

    public override EActionStatus Perform()
    {
        if (hitOngoing)
        {
            if (delayTime <= 0)
                hitOngoing = false;

            return EActionStatus.Running;
        }
        
        else
            return EActionStatus.Success;
    }

    public override bool PostPerform()
    {
        return base.PostPerform();
    }

    public override bool PrePerform()
    {        
        hitOngoing = true;
        enemy.AnimManager.SetAttack(1); //MAGIC NUMBER
        weaponAnimation.OnAttackExecuting += HitDone;
        return base.PrePerform();
    }

    private void HitDone(bool isRunning)
    {
        if(!isRunning)
        {
            weaponAnimation.OnAttackExecuting -= HitDone;
            delayTime = Random.Range(4, 10);         
        }
    }


    private void Update()
    {
        if(delayTime > 0)
            delayTime -= 1; //frame   
    }
}
