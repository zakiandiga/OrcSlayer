using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : AnimationManager
{
    [SerializeField] protected EnemyAnimationHolder animList;

    public override void SetFallingFloat(float value)
    {
        anim.SetFloat(animList.fallFloat, value);
    }

    public override void SetRunningBool(bool isRunning)
    {
        anim.SetBool(animList.runningBool, isRunning);
    }

    public override void SetRunningFloat(float value)
    {
        anim.SetFloat(animList.runningFloat, value);
    }

    public override void SetTriggerJump()
    {
        anim.Play(animList.jumpTrigger);
    }

    public override void SetTriggerLand()
    {
        anim.Play(animList.landTrigger);
    }

    public void SetAggroBool(bool isAggro)
    {
        anim.SetBool(animList.isAggro, isAggro);
    }

    public void SetAttack(int comboCount)
    {
        switch (comboCount)
        {
            case 1:
                anim.Play(animList.normalAttack01);
                break;
            case 2:
                anim.Play(animList.normalAttack02);
                break;
            case 5:
                anim.Play(animList.specialAttack01);
                break;
        }
    }
}
