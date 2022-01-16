using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashNormalAttack : ActionState
{
    public PlayerDashNormalAttack(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }


    public override void Enter()
    {
        base.Enter();

        player.SetCurrentDamage(1);
        player.Anim.Play(playerAnimation.normalSlideAttack);

        comboCount += 2;

        actionFinished = false;
    }

    public override void Exit()
    {
        base.Exit();

        actionFinished = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Slide();   

        if(Mathf.Abs(horizontalVelocity) < 0.08f)
        {
            horizontalVelocity = 0;
            stateMachine.ChangeState(player.IdleState);
        }

        SetPlayerHorizontalVelocity(horizontalVelocity, playerData.groundSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Slide()
    {
        SpeedChange(0, playerData.deccelTime);
    }
}
