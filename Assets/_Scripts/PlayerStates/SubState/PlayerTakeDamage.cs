using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : ActionState
{
    public PlayerTakeDamage(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {
    }

    private float staggerTime;
    private string staggerTimer = "StaggerTimer";

    public override void Enter()
    {
        base.Enter();
        player.Anim.Play(playerAnimation.takeDamage);
        //Debug.Log("Player take damage from " + stateMachine.LastState);
        actionFinished = false;

        staggerTime = playerData.staggerTime;

        if(!Timer.TimerRunning(staggerTimer)) 
            Timer.Create(TakeDamageFinishing, staggerTime, staggerTimer);

    }

    public override void Exit()
    {
        base.Exit();

        actionFinished = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!player.IsGrounded)
            Fall();
    }

    private void TakeDamageFinishing()
    {
        if (player.IsGrounded)
            stateMachine.ChangeState(player.IdleState);
        else
            stateMachine.ChangeState(player.FallState);
    }
    
    private void Fall()
    {
        verticalVelocity += (playerData.gravityValue + playerData.airAttackGravityMod) * Time.deltaTime;
        player.SetVelocityY(verticalVelocity);
    }
}
