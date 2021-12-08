using UnityEngine;
using SGoap;

public class SpinAttack : BasicAction
{
    private EnemyBehaviour enemy;

    private bool hitOngoing = false;

    private WeaponAnimationEvents weaponAnimation;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
        weaponAnimation = enemy.GetComponentInChildren<WeaponAnimationEvents>();
    }

    public override EActionStatus Perform()
    {
        if(hitOngoing)
        {
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
        weaponAnimation.OnAttackDone += HitDone;
        return base.PrePerform();
    }

    private void HitDone(bool isDone)
    {
        weaponAnimation.OnAttackDone -= HitDone;
        hitOngoing = false;
    }
}
