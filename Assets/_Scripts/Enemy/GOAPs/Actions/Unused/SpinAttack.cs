using UnityEngine;
using SGoap;

public class SpinAttack : EnemyAction
{
    private bool hitOngoing = false;
    private int delayTime = 0;

    public override float CooldownTime { get { return 5; } }

    public override EActionStatus Perform()
    {
        if(hitOngoing)
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
        enemy.AnimManager.SetAttack(5); //look at the EnemyAnimationManager for the value, refactor this system later
        weaponAnimation.OnAttackExecuting += HitDone;
        return base.PrePerform();
    }

    private void HitDone(bool isRunning)
    {
        if (!isRunning)
        {
            weaponAnimation.OnAttackExecuting -= HitDone;
            enemy.StaminaUsed(5);
            delayTime = 10;
        }

    }

    private void Update()
    {
        if (delayTime > 0)
            delayTime -= 1;
    }
}
