using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : AirState
{

    public PlayerFall(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    private float jumpTolerance = 0.3f;
    private float jumpToleranceTimer;

    private bool jumpCountAdded = false;

    public override void Enter()
    {
        base.Enter();

        jumpCountAdded = false;
        jumpToleranceTimer = jumpTolerance;           
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    { 
        base.LogicUpdate();

        if(stateMachine.LastState != player.AirNormalAttackState)
        {
            player.Anim.SetFloat("falling", verticalVelocity);
        }
        
        SpeedChange(moveInputAxis, playerData.airAccelTime);
        SetPlayerHorizontalVelocity(horizontalVelocity, playerData.airSpeed);                       

        if(stateMachine.LastState != player.JumpState)
        {
            jumpToleranceTimer -= Time.deltaTime;
            if (jumpToleranceTimer >= 0 && player.JumpCount < playerData.maxJumpCount && isJumping)
            {
                jumpToleranceTimer = 0;
                player.InputHandler.JumpStop();
                stateMachine.ChangeState(player.JumpState);
            }
            else if (jumpToleranceTimer < 0 && !jumpCountAdded && !isJumping)
            {
                player.AddJumpCount(1);
                jumpCountAdded = true;
            }

        }
        

        if (player.JumpCount < playerData.maxJumpCount && isJumping)
        {
            player.InputHandler.JumpStop();
            stateMachine.ChangeState(player.JumpState);
        }

        if (normalAttackInput)
        {
            stateMachine.ChangeState(player.AirNormalAttackState);
        }

        if (player.IsGrounded)
        {
            stateMachine.ChangeState(player.LandState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
