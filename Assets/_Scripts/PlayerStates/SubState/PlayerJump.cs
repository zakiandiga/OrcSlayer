using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : ActionState
{
    public PlayerJump(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }


    public override void Enter()
    {
        base.Enter();
        actionFinished = false;
        Jump();    
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }


    private void Jump()
    {        
        verticalVelocity = 0; //make sure the vertical velocity is 0 before jump performed

        player.AddJumpCount(1);
        verticalVelocity += Mathf.Sqrt(playerData.jumpHeight * playerData.jumpConst * playerData.gravityValue);
        player.SetVelocityY(verticalVelocity);
        //player.Anim.SetTrigger("jump");
        player.Anim.Play(playerAnimation.jumpTrigger);

        actionFinished = true;
    }
}
