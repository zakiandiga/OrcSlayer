using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDies : ActionState
{
    public PlayerDies(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Anim.Play(playerAnimation.dies);
        player.InputHandler.InputActionSwitch(false);
    }
}
