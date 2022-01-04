using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : AirState
{

    public PlayerFall(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    private string coyoteJumpTimer = "CoyoteJumpTimer";
    private float coyoteTime = 0.3f;
    private bool canCoyote = false;
    private float jumpTolerance = 0.3f;

    private bool jumpCountAdded = false;

    public override void Enter()
    {
        base.Enter();

        jumpCountAdded = false;         
    }

    public override void Exit()
    {
        base.Exit();
        Timer.ForceStopTimer(coyoteJumpTimer);
        if (canCoyote)
            canCoyote = false;
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

        //Coyote effect
        if(stateMachine.LastState != player.JumpState && stateMachine.LastState != player.LandState)
        {
            if(!Timer.TimerRunning(coyoteJumpTimer))
            {
                Timer.Create(CoyoteSwitch, coyoteTime, coyoteJumpTimer);
                canCoyote = true;
            }

            if(onJumpPressedTolerance && canCoyote && player.JumpCount < playerData.maxJumpCount)
                stateMachine.ChangeState(player.JumpState);
            
            else if (!onJumpPressedTolerance && !canCoyote && !jumpCountAdded)
            {
                player.AddJumpCount(1);
                jumpCountAdded = true;
            }
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

    private void CoyoteSwitch()
    {
        if (canCoyote)
            canCoyote = false;
    }
}
