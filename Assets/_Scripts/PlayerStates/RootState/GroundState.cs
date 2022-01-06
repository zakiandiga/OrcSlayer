using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : PlayerState
{    
    public GroundState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(player.IsGrounded && onJumpPressedTolerance)
        {
            //player.InputHandler.JumpStop();
            if (isTurning)
            {
                isTurning = false;
                ForceTurning();
            }
            stateMachine.ChangeState(player.JumpState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }
}
