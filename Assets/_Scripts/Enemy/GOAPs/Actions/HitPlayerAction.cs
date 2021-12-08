using UnityEngine;
using SGoap;

public class HitPlayerAction : BasicAction
{
    private EnemyBehaviour enemy;

    private bool hitOngoing = false;
    private int delayTime = 0;

    private WeaponAnimationEvents weaponAnimation;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
        weaponAnimation = enemy.GetComponentInChildren<WeaponAnimationEvents>();
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
        weaponAnimation.OnAttackDone += HitDone;
        return base.PrePerform();
    }

    private void HitDone(bool isDone)
    {
        if(isDone)
        {
            weaponAnimation.OnAttackDone -= HitDone;
            delayTime = Random.Range(4, 10);         
        }
    }


    private void Update()
    {
        if(delayTime > 0)
            delayTime -= 1; //frame   
    }
}